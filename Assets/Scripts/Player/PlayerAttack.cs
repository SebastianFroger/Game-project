using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Shooter
{
    public class PlayerAttack : MonoBehaviour
    {
        public GameObject bullet;
        public FloatVariable attacksInterval;
        public InAttackRange inAttackRange;

        private float _nextAttackTime = 0f;
        private Transform _nearestEnemy;
        private float _smallestDistance;
        private float _distance;
        private GameObject _bulletInst;

        private void Update()
        {
            if (Time.time < _nextAttackTime || inAttackRange.enemies.Count == 0) return;
            _nextAttackTime = Time.time + attacksInterval.Value;

            _smallestDistance = Mathf.Infinity;
            foreach (var enemy in inAttackRange.enemies)
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

        private void OnTriggerEnter(Collider other)
        {
            inAttackRange.Add(other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            inAttackRange.Remove(other.transform);
        }

    }
}