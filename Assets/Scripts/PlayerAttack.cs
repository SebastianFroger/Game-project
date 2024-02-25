using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerData playerData;
    public Transform lookTarget;

    private Health _enemyHealth;
    private float _nextAttackTime = 0f;

    private void Update()
    {
        transform.rotation = lookTarget.rotation;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        if (Time.time < _nextAttackTime) return;

        if (_enemyHealth == null)
            _enemyHealth = other.gameObject.GetComponent<Health>();

        _enemyHealth.TakeDamage(playerData.damage);

        _nextAttackTime = Time.time + playerData.attackInterval;
    }
}
