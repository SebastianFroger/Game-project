using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;


public class Bullet : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public float distance = 100f;
    public int speed = 100;
    public GameObject hitEffect;
    public UnityEvent OnHitEvent;
    public LayerMask layerMask;
    public bool useRaycast;
    public float rotate;

    private Vector3 startPosition;
    private Vector3 _prevPosition;
    private float _damage;
    private IHealth _targetHP;
    private List<Collider> _hitColliders = new List<Collider>();

    private void OnEnable()
    {
        startPosition = transform.position;
        _prevPosition = GlobalObjectsManager.Instance.player.transform.position;
        _hitColliders.Clear();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector3.Distance(startPosition, transform.position) >= distance)
            MyObjectPool.Instance.Release(gameObject);

        if (!useRaycast) return;

        // Check if the bullet hit something, by using a linecast from previous position to current position
        if (Physics.Linecast(_prevPosition, transform.position, out RaycastHit hit, layerMask))
        {
            if (_hitColliders.Contains(hit.collider)) return;

            _damage = unitStatsSO.damage.value;
            if (Random.Range(0f, 100f) <= unitStatsSO.critChance.value)
                _damage *= 1.5f;

            _targetHP = hit.collider.gameObject.GetComponent<IHealth>();

            // check if we hit environment
            if (_targetHP == null)
            {
                MyObjectPool.Instance.Release(gameObject);
                return;
            }

            _targetHP.TakeDamage(_damage);

            // energysteal
            if (unitStatsSO.energySteal.value > 0)
                BatteryManager.Instance.AddToAllBatteries(unitStatsSO.energySteal.value / 3f);


            // slow enemy
            if (unitStatsSO.slowEnemies.value < 0)
                hit.collider.gameObject.GetComponent<EnemyControl>().SlowDown(unitStatsSO.slowEnemies.value);

            // knock back
            if (unitStatsSO.knockBackEnemies.isActive)
                hit.collider.gameObject.GetComponent<EnemyControl>().KnockBack(unitStatsSO.knockBackEnemies.value);

            // piercing
            if (_hitColliders.Count < unitStatsSO.piercingCount.value)
            {
                if (!_hitColliders.Contains(hit.collider))
                {
                    _hitColliders.Add(hit.collider);
                    return;
                }

                OnHitEvent?.Invoke();
            }
            else
            {
                // relsease bullet
                MyObjectPool.Instance.Release(gameObject);
            }

            // hit effect
            MyObjectPool.Instance.GetInstance(hitEffect, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
        }

        _prevPosition = transform.position;
    }
}


