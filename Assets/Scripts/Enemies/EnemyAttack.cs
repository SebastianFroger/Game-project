using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class EnemyAttack : MonoBehaviour
    {
        public int damage = 10;
        public float interval = 2f;

        private Health _health;
        private float _nextAttackTime = 0f;


        private void OnCollisionStay(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            if (Time.time < _nextAttackTime) return;

            if (_health == null)
                _health = other.gameObject.GetComponent<Health>();

            _health.TakeDamage(damage);

            _nextAttackTime = Time.time + interval;
        }
    }
}