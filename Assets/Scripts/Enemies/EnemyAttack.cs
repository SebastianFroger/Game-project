using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;

    private Health _health;
    private float _nextAttackTime = 0f;


    private void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (Time.time < _nextAttackTime) return;

        if (_health == null)
            _health = other.gameObject.GetComponent<Health>();

        _health.TakeDamage(unitStatsSO.dammage);

        _nextAttackTime = Time.time + (1 / unitStatsSO.attackSpeed);
    }
}
