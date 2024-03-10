using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public bool rangedAttack;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    private Health _health;
    private float _nextAttackTime = 0f;
    private bool _inRange;
    private GameObject _bulletInst;
    private Transform _player;
    private EnemyControl _enemyControl;

    private void OnEnable()
    {
        if (_enemyControl == null)
            _enemyControl = GetComponent<EnemyControl>();
        _enemyControl.stopped = false;
    }

    private void Update()
    {
        if (!rangedAttack) return;

        if (Time.time > _nextAttackTime && _inRange)
        {
            _nextAttackTime = Time.time + (1f / unitStatsSO.attackSpeed);

            // fire
            _bulletInst = MyObjectPool.Instance.GetInstance(bulletPrefab);
            _bulletInst.transform.position = bulletSpawn.position;
            _bulletInst.transform.LookAt(_player);
        }
    }

    // ranged
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _enemyControl.stopped = true;
        _inRange = true;
        if (_player == null)
            _player = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _enemyControl.stopped = false;
        _inRange = false;
    }

    // melee
    private void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (!rangedAttack)
        {
            _enemyControl.enabled = false;
            if (Time.time < _nextAttackTime) return;

            if (_health == null)
                _health = other.gameObject.GetComponent<Health>();

            _health.TakeDamage(unitStatsSO.dammage);

            _nextAttackTime = Time.time + (1 / unitStatsSO.attackSpeed);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (!rangedAttack)
        {
            _enemyControl.stopped = false;
        }
    }
}
