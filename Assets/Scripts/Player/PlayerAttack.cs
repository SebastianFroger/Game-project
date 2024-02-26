using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject bullet;

    private float _nextAttackTime = 0f;
    private List<Transform> _nearbyEnemies = new();
    private Transform _nearestEnemy;
    private float _smallestDistance;
    private float _distance;
    private GameObject _bulletInst;

    private void Start()
    {
        EnemyHealth.OnDeath += RemoveDeadEnemy;
    }

    private void Update()
    {
        if (Time.time < _nextAttackTime || _nearbyEnemies.Count == 0) return;
        _nextAttackTime = Time.time + playerData.attacksPerSec;

        _smallestDistance = Mathf.Infinity;
        foreach (var enemy in _nearbyEnemies)
        {
            _distance = Vector3.Distance(transform.position, enemy.position);
            if (_distance > _smallestDistance) continue;
            _smallestDistance = _distance;
            _nearestEnemy = enemy;
        }

        // fire
        _bulletInst = MyObjectPool.GetInstance(bullet, MyObjectPool.bullet);
        _bulletInst.transform.position = transform.position;
        _bulletInst.transform.LookAt(_nearestEnemy);
    }

    public void RemoveDeadEnemy(Transform obj)
    {
        if (_nearbyEnemies.Contains(obj))
            _nearbyEnemies.Remove(obj);
    }

    private void OnTriggerEnter(Collider other)
    {
        _nearbyEnemies.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        _nearbyEnemies.Remove(other.transform);
    }

}
