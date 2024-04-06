using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityBody : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody _rb;
    public float gravity = -10;
    public float gravityFallIncrease = -1;

    private Vector3 _gravityDir;
    public bool isInside = true;
    public bool inTransition;
    public LayerMask layerMask;
    private float jumpDistance;
    private float lastJumpDistance;

    private PlayerControl _playerControl;

    void Start()
    {
        _playerControl = GetComponent<PlayerControl>();
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.useGravity = false;
        _transform = transform;

        inTransition = false;
    }

    void FixedUpdate()
    {
        Attract(_transform, _rb);
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
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.up * 2);

        Gizmos.color = Color.white;
        Gizmos.DrawRay(Vector3.zero, -_gravityDir * 10);
    }
}