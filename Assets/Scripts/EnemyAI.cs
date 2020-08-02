using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float turnSpeed = 5f;
    private float _distanceToTarget = Mathf.Infinity;
    private bool _isProvoked = false;

    private NavMeshAgent _navMeshAgent;
    private static readonly int Attack = Animator.StringToHash("attack");
    private static readonly int Move = Animator.StringToHash("move");

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        _distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (_isProvoked)
        {
            EngageTarget();
        }
        else if (PlayerInRange(_distanceToTarget))
        {
            _isProvoked = true;
        }
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (!InAttackRange())
        {
            ChaseTarget();
        }

        if (InAttackRange())
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool(Attack, true);
        // print($"{name} attacking {target} at distance {_distanceToTarget}");
    }

    private bool InAttackRange()
    {
        // seemed to need the .5 fudge factor, since the enemy seems to be
        //   stopping just outside the stopping distance.
        return _distanceToTarget <= _navMeshAgent.stoppingDistance + .5;
    }

    private bool PlayerInRange(float distanceToTarget)
    {
        return distanceToTarget <= chaseRange;
    }

    private void ChaseTarget()
    {
        var animator = GetComponent<Animator>();
        // start with attack/false in case we run out of attack range of the enemy
        animator.SetBool(Attack, false);
        animator.SetTrigger(Move);
        var result = _navMeshAgent.SetDestination(target.position);
    }

    private void FaceTarget()
    {
        // https://docs.unity3d.com/ScriptReference/Vector3-normalized.html, 
        // ensure magnitude is 1 (we only want to affect direction not magnitude)
        var direction = (target.position - transform.position).normalized;
        // y = 0; we are not looking up/down
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // https://docs.unity3d.com/ScriptReference/Vector3.Slerp.html
        // Spherically interpolates between two vectors. Interpolates between a and b by amount t. 
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected()
    {
        // Display the chase radius when selected
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawSphere(transform.position, chaseRange);
    }
}