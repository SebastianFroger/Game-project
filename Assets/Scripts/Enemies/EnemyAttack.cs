using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAttack : Attack
{
    public UnitStatsSO unitStatsSO;
    public bool rangedAttack;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    private IHealth _health;
    private float _nextAttackTime = 0f;
    private GameObject _bulletInst;
    private Transform _player;

    private void Start()
    {
        _player = GlobalObjectsManager.Instance.player.transform;
    }

    private void Update()
    {
        CheckIfInRange();

        if (!inAttackRange)
            return;

        if (Time.time > _nextAttackTime)
        {
            _nextAttackTime = Time.time + (1f / unitStatsSO.attacksPerSecond);

            if (rangedAttack)
            {
                // fire
                _bulletInst = MyObjectPool.Instance.GetInstance(bulletPrefab);
                _bulletInst.transform.position = bulletSpawn.position;
                _bulletInst.transform.LookAt(_player);
            }
            else
            {
                // melee
                if (_health == null)
                    _health = _player.GetComponent<IHealth>();
                _health.TakeDamage(unitStatsSO.damage);
            }
        }
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
