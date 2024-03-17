using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Bullet : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public float distance = 100f;
    public int speed = 100;
    public GameObject hitEffect;
    public UnityEvent OnHitEvent;

    private Vector3 startPosition;
    private Vector3 _prevPosition;

    private Vector3 collisionPoint;
    private Vector3 collisionNormal;

    private void OnEnable()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        _prevPosition = transform.position;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector3.Distance(startPosition, transform.position) >= distance)
            MyObjectPool.Instance.Release(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnHitEvent?.Invoke();
        other.gameObject.GetComponent<Health>()?.TakeDamage(unitStatsSO.dammage);
        MyObjectPool.Instance.Release(gameObject);

        var inst = MyObjectPool.Instance.GetInstance(hitEffect);
        collisionPoint = other.ClosestPoint(_prevPosition);
        collisionNormal = _prevPosition - collisionPoint;
        inst.transform.position = collisionPoint;
        inst.transform.rotation = Quaternion.FromToRotation(Vector3.forward, collisionNormal.normalized);
    }
}
