using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosionAttack : Attack
{
    public UnitStatsSO unitStatsSO;
    public float explosionRange;
    public LayerMask layerMask;

    private Health _health;
    private Transform _player;

    private void Start()
    {
        _health = GetComponent<Health>();
        _player = GlobalObjectsManager.Instance.player.transform;
    }

    private void Update()
    {
        CheckIfInRange();
        if (inAttackRange)
        {
            Explode();
        }
    }

    // called by Healt OnDeathEvent
    public void Explode()
    {
        var colliders = Physics.OverlapSphere(transform.position, explosionRange, layerMask);
        foreach (var item in colliders)
        {
            item.gameObject.GetComponent<IHealth>().TakeDamage(unitStatsSO.damage);
        }

        _health.TakeDamage(500);
    }

    // calculate distance to player if less than attack range return true
    void CheckIfInRange()
    {
        inAttackRange = false;
        if (Vector3.Distance(transform.position, _player.position) < attackRange)
        {
            inAttackRange = true;
        }
    }
}
