using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityBody : MonoBehaviour
{
    public float gravity = -10;
    public float gravityFallIncrease = -1;
    public float slerpValue = -10;

    private Rigidbody _rb;
    private Vector3 _gravityDir;
    public bool isInside = true;
    public LayerMask layerMask;

    private PlayerControl _playerControl;

    void Start()
    {
        _playerControl = GetComponent<PlayerControl>();
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Attract(transform, _rb);
    }

    public void Attract(Transform body, Rigidbody rb)
    {
        if (!_playerControl.onGround)
        {
            if (!isInside)
            {
                // shoot ray away from player in zero direction
                if (Physics.Raycast(body.position, -transform.position, out RaycastHit hit1, 30, layerMask))
                {
                    _gravityDir = hit1.normal.normalized;
                }
            }
            else
            {
                // shoot ray away from zero in player direction
                if (Physics.Raycast(body.position, transform.position, out RaycastHit hit2, 30, layerMask))
                {
                    _gravityDir = hit2.normal.normalized;
                }
            }
        }
        else
        {
            // player on ground force
            if (Physics.Raycast(body.position, -transform.up * 2, out RaycastHit hit, 30, layerMask))
            {
                _gravityDir = hit.normal.normalized;
            }
        }


        // adjust jump fall force
        if (!_playerControl.onGround)
            gravity += gravityFallIncrease;
        else
            gravity = -10;


        rb.AddForce(_gravityDir * gravity);
        Quaternion targetRotation = Quaternion.FromToRotation(body.up, _gravityDir) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, slerpValue);
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