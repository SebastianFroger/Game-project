using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosionAttack : Attack
{
    public UnitStatsSO unitStatsSO;
    public float explosionRange;
    public LayerMask layerMask;
    public float explosionDelay = 2f;

    private Health _health;
    private Transform _player;
    Light blinkLight;

    private void Awake()
    {
        blinkLight = GetComponentInChildren<Light>();
        blinkLight.intensity = 0;

        _health = GetComponent<Health>();
        _player = GlobalObjectsManager.Instance.player.transform;
        armed = false;
    }

    private void OnEnable()
    {
        blinkLight.intensity = 0;
        armed = false;
    }

    IEnumerator ExplosionDelayed()
    {
        var explosionTimer = Time.time + explosionDelay;

        while (Time.time < explosionTimer)
        {
            blinkLight.intensity = 0;
            yield return new WaitForSeconds(0.25f);
            blinkLight.intensity = 5;
            yield return new WaitForSeconds(0.25f);
        }

        Explode();
    }

    private void Update()
    {
        CheckIfInRange();
        if (inAttackRange && !armed)
        {
            armed = true;
            StartCoroutine(ExplosionDelayed());
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
