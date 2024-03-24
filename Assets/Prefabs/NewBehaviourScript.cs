using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        var rotTowards = (Vector3.RotateTowards(transform.position, player.position, 0.1f, 0.1f) - transform.position);
        var moveDir = rotTowards.normalized * 5 * Time.deltaTime;


        // transform.position = moveDir + transform.position;


        // var target = Quaternion.LookRotation(moveDir - transform.position);
        // transform.rotation = target;
        transform.LookAt(player);

        Debug.DrawRay(transform.position, player.position, Color.red);
    }
}
