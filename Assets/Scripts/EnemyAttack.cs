using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerHealth _target;
    [SerializeField] private float damage = 40f;

    private void Start()
    {
        _target = FindObjectOfType<PlayerHealth>();
    }

    /// <summary>
    /// animation event
    /// </summary>
    public void AttackHitEvent()
    {
        if (_target == null){return;}
        _target.TakeDamage(damage);
        _target.GetComponent<DisplayDamage>().SowDamageImpact();
    }

}
