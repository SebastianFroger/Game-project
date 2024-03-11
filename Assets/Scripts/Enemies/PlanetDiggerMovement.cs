using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDiggerMovement : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public float groundHeightAdjust = 1f;

    private bool _hasLanded;

    private void Start()
    {
        transform.LookAt(Vector3.zero, transform.up);
    }

    private void OnEnable()
    {
        _hasLanded = false;
    }

    private void Update()
    {
        if (!_hasLanded)
        {
            transform.position += -transform.position.normalized * unitStatsSO.speed * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position.normalized * (Planet.Instance.GetRadius() + groundHeightAdjust);
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
