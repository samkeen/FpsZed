using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float chaseRange = 5f;
    private float _distanceToEnemy = Mathf.Infinity;

    private NavMeshAgent _navMeshAgent;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (PlayerInRange())
        {
            NavigateToPlayer();
        }
    }

    private bool PlayerInRange()
    {
        return Vector3.Distance(player.position, transform.position) <= chaseRange;
    }

    private void NavigateToPlayer()
    {
        _navMeshAgent.SetDestination(player.position);
    }
}
