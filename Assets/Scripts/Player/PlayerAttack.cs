using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;
    public UnitStatsSO unitStatsSO;
    public EnemiesInRange _enemiesInRange;

    private float _nextAttackTime;
    [SerializeField] private Transform _nearestEnemy;
    private float _smallestDistance;
    private float _distance;
    private GameObject _bulletInst;

    private void Update()
    {
        if (Time.time < _nextAttackTime || _enemiesInRange.Items.Count == 0) return;
        _nextAttackTime = Time.time + (1f / unitStatsSO.attackSpeed);

        _smallestDistance = Mathf.Infinity;
        foreach (var e in _enemiesInRange.Items)
        {
            _distance = Vector3.Distance(transform.position, e.position);
            if (_distance > _smallestDistance) continue;
            _smallestDistance = _distance;
            _nearestEnemy = e;
        }

        // fire
        _bulletInst = MyObjectPool.Instance.GetInstance(bullet, MyObjectPool.Instance.bullet);
        _bulletInst.transform.position = transform.position;
        _bulletInst.transform.LookAt(_nearestEnemy);
    }

    private void OnTriggerEnter(Collider other)
    {
        _enemiesInRange.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        _enemiesInRange.Remove(other.transform);
    }
}