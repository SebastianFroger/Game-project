using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform planet;
    public float speed = 1f;
    public Vector3 direction = Vector3.forward;

    private Vector3 _prevPosition;
    
    public void Update()
    {
        Move(direction);
    }

    public void Move(Vector3 direction)
    {
        _prevPosition = transform.position;
        transform.RotateAround(planet.transform.position, direction, speed * Time.deltaTime); 
        transform.rotation = Quaternion.LookRotation(transform.position - _prevPosition, transform.position - planet.position);
    }
}
