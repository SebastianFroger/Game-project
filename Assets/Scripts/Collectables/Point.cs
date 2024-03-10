using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Point : MonoBehaviour
{
    public UnitStatsSO unitStats;
    public int value = 1;

    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        unitStats.points += 1;
        MyObjectPool.Instance.points.Release(gameObject);
    }
}
