using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class GravityBody : MonoBehaviour
    {
        public GlobalManagerSO globalManagerSO;

        private Transform _transform;
        private Rigidbody _rb;


        void Start()
        {
            if (globalManagerSO.attractor == null)
                DebugExt.LogError(this, "Missing globalManagerSO");

            _rb = GetComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
            _rb.useGravity = false;
            _transform = transform;
        }

        void Update()
        {
            if (globalManagerSO.attractor != null)
                globalManagerSO.attractor.Attract(_transform, _rb);
        }
    }
}