using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityBody : MonoBehaviour
{
    public GravityAttractor gravityAttractor;

    private Transform _transform;
    private Rigidbody _rb;


    void Start()
    {
        if (gravityAttractor == null)
            DebugExt.LogError(this, "Missing gravityAttractor");

        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.useGravity = false;
        _transform = transform;
    }

    void Update()
    {
        if (gravityAttractor != null)
            gravityAttractor.Attract(_transform, _rb);
    }
}