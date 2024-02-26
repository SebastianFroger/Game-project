using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityBody : MonoBehaviour
{
    public GravityAttractor attractor;

    private Transform _transform;
    private Rigidbody _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.useGravity = false;
        _transform = transform;
        attractor = ObjectManager.attractor;
    }

    void Update()
    {
        attractor.Attract(_transform, _rb);
    }
}