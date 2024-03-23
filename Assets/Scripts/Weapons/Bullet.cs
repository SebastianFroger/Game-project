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

    private Vector3 startPosition;
    private Vector3 _prevPosition;

    private void OnEnable()
    {
        startPosition = transform.position;
        _prevPosition = Vector3.zero;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector3.Distance(startPosition, transform.position) >= distance)
            MyObjectPool.Instance.Release(gameObject);

        if (!useRaycast) return;

        // Check if the bullet hit something, by using a linecast from previous position to current position
        // fast bullets
        if (_prevPosition == Vector3.zero)
        {
            _prevPosition = transform.position;
            return;
        }

        if (Physics.Linecast(_prevPosition, transform.position, out RaycastHit hit, layerMask))
        {
            var dammage = unitStatsSO.dammage.value;
            if (Random.Range(0f, 100f) <= unitStatsSO.critChance.value)
                dammage *= 1.5f;
            hit.collider.gameObject.GetComponent<IHealth>()?.TakeDamage(dammage);

            OnHitEvent?.Invoke();
            MyObjectPool.Instance.Release(gameObject);

            // hit effect
            MyObjectPool.Instance.GetInstance(hitEffect, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
        }

        _prevPosition = transform.position;
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (!other.CompareTag("Player")) return;

    //     other.gameObject.GetComponent<IHealth>()?.TakeDamage(unitStatsSO.dammage.value);

    //     OnHitEvent?.Invoke();
    //     MyObjectPool.Instance.Release(gameObject);

    //     MyObjectPool.Instance.GetInstance(hitEffect, other, Quaternion.FromToRotation(Vector3.forward, hit.normal));
    // }
}


