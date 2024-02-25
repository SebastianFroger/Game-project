using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GravityAttractor : MonoBehaviour
{
    public float gravity = -10;

    private Vector3 _gravityUp;

    public static GravityAttractor attractor;

    private void Start()
    {
        attractor = this;
    }

    public void Attract(Transform body, Rigidbody rb)
    {
        _gravityUp = (body.position - transform.position).normalized;
        rb.AddForce(_gravityUp * gravity);
        Quaternion targetRotation = Quaternion.FromToRotation(body.up, _gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }

}