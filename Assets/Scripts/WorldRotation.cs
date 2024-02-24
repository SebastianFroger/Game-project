using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldRotation : MonoBehaviour
{
    public Transform player;
    public float speed;

    void OnMove(InputValue value)
    {
        // var input = value.Get<Vector2>();
        // Debug.Log("input " + input);

        // var current = transform.position - player.position;
        // Debug.Log("current " + current);

        // var target = transform.position - (player.position + new Vector3(input.x, 0f, input.y));
        // Debug.Log("target " + target);
        
        // float singleStep = speed * Time.deltaTime;
        // var newDir = Vector3.RotateTowards(current, target, singleStep, 0.0f);

        // transform.rotation = Quaternion.Lerp(Quaternion.Euler(current), Quaternion.Euler(newDir), singleStep);
    }
}
