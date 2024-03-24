using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosionAttack : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public float explosionRange;
    public GameObject particle;
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

        var go = MyObjectPool.Instance.GetInstance(particle, transform.position, transform.rotation);
        var _particleEffect = go.GetComponent<ParticleSystem>();
        _particleEffect.Play();

        MyObjectPool.Instance.Release(gameObject, _particleEffect.main.duration);
    }
}
