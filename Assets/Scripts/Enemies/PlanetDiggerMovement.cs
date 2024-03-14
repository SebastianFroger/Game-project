using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDiggerMovement : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public TransformRuntimeSet transformRuntimeSet;
    public float groundHeightAdjust = 0.2f;

    private bool _hasLanded;
    private Rigidbody _rb;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        transform.LookAt(Vector3.zero);
        _hasLanded = false;
    }

    private void FixedUpdate()
    {
        if (!_hasLanded)
        {
            transform.LookAt(Vector3.zero);
            _rb.velocity = transform.forward * unitStatsSO.speed;
        }
        else
        {
            return;
            _rb.MovePosition(transform.position.normalized * (Planet.Instance.GetRadius() + groundHeightAdjust));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Planet"))
        {
            _hasLanded = true;
            _rb.isKinematic = true;

            foreach (var item in transformRuntimeSet.Items)
            {
                if (item == transform)
                    return;
            }
            transformRuntimeSet.Items.Add(transform);
            DebugExt.Log(this, $"add");
        }
    }

    private void OnDisable()
    {
        transformRuntimeSet.Items.Remove(transform);
    }
}
