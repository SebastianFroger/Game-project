using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosionAttack : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public float explosionRange;
    public LayerMask layerMask;

    private Health _health;

    private void Start()
    {
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _health.TakeDamage(500);
    }

    // called by Healt OnDeathEvent
    public void Explode()
    {
        var colliders = Physics.OverlapSphere(transform.position, explosionRange, layerMask);
        foreach (var item in colliders)
        {
            item.gameObject.GetComponent<IHealth>().TakeDamage(unitStatsSO.damage.value);
        }

        _health.TakeDamage(500);
    }
}
