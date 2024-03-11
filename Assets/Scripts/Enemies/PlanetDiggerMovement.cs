using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDiggerMovement : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public float groundHeightAdjust = 1f;

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
            _rb.MovePosition(-transform.position.normalized * unitStatsSO.speed * Time.deltaTime);
        }
        else
        {
            _rb.MovePosition(transform.position.normalized * (Planet.Instance.GetRadius() + groundHeightAdjust));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Planet"))
        {
            _hasLanded = true;
            PlanetDiggerManager.Instance.AddDigger();
        }
    }

    private void OnDisable()
    {
        PlanetDiggerManager.Instance.RemoveDigger();
    }
}
