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
        _damage = StatsManager.Instance.OnShot();
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

            _targetHP = hit.collider.gameObject.GetComponent<IHealth>();

            // check if we hit environment
            if (_targetHP == null)
            {
                MyObjectPool.Instance.Release(gameObject);
                return;
            }

            _targetHP.TakeDamage(_damage);

            // slow enemy
            if (unitStatsSO.enemySlowPercentage < 0)
                hit.collider.gameObject.GetComponent<EnemyControl>().SlowDown(unitStatsSO.enemySlowPercentage);

            // knock back
            if (unitStatsSO.enemyKnockBackForce > 0)
                hit.collider.gameObject.GetComponent<EnemyControl>().KnockBack(unitStatsSO.enemyKnockBackForce);

            // piercing
            if (_hitColliders.Count < unitStatsSO.piercingCount)
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


