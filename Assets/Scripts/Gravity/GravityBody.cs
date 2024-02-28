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
            _rb = GetComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
            _rb.useGravity = false;
            _transform = transform;
        }

        void Update()
        {
            globalManagerSO.attractor.Attract(_transform, _rb);
        }
    }
}