using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityAttractor : MonoBehaviour
{
    public float gravity = -10;

    private Vector3 _gravityUp;
    private Vector3 _bodyUp;
    

    public void Attract(Transform body, Rigidbody rb)
    {
        _gravityUp = (body.position - transform.position).normalized;
        _bodyUp = body.up;

        rb.AddForce(_gravityUp  * gravity);

        Quaternion targetRotation = Quaternion.FromToRotation(_bodyUp, _gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }

}