using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityBody : MonoBehaviour
{
    public float gravity = -10;
    public float gravityFallIncrease = -1;
    public float slerpValue = -10;
    public bool isPlayer;
    public LayerMask layerMask;
    public bool isInside = true;

    private Rigidbody _rb;
    private Vector3 _gravityDir;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.useGravity = false;

        transform.LookAt(Vector3.zero, transform.forward);
        transform.Rotate(-90, 0, 0);
    }

    private void OnEnable()
    {
        // rotate towards zero in tramsform.up direction
        transform.LookAt(Vector3.zero, transform.forward);
        transform.Rotate(-90, 0, 0);
    }

    void FixedUpdate()
    {
        Attract();
    }

    public void Attract()
    {
        if (Physics.Raycast(transform.position, -transform.up * 5, out RaycastHit hit2, 30, layerMask))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit2.normal.normalized) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, slerpValue);
        }

        _rb.AddForce(transform.up * gravity);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("InsidePlanetTrigger"))
        {
            isInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InsidePlanetTrigger"))
        {
            isInside = false;
        }
    }
}