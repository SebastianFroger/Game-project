using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, -31, 9);
    // Update is called once per frame
    void Update()
    {
        var player = GlobalObjectsManager.Instance.player;
        var dir = Quaternion.Euler(player.transform.position.normalized);
        transform.position = dir * player.transform.position;
        transform.position += transform.forward * offset.y;
        transform.position += transform.up * offset.z;
    }
}
