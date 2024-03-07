using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityBody : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.useGravity = false;
        _transform = transform;
    }

    void Update()
    {
        GlobalObjectsManager.Instance.gravityAttractor.Attract(_transform, _rb);
    }
}