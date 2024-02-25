using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public float interval = 2f;

    private PlayerHealth _playerHealth;
    private float _nextAttackTime = 0f;


    private void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (Time.time < _nextAttackTime) return;

        if (_playerHealth == null)
            _playerHealth = other.gameObject.GetComponent<PlayerHealth>();

        _playerHealth.TakeDamage(damage);

        _nextAttackTime = Time.time + interval;
    }
}
