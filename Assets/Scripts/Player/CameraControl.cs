using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector3 posOffset;
    GameObject player;

    private void Start()
    {
        player = GlobalObjectsManager.Instance.player;
        posOffset = transform.localPosition;
        transform.parent = null;
    }

    void Update()
    {
        var position = player.transform.position;
        position = new Vector3(position.x, 0, position.z);
        position += posOffset;
        transform.position = position;
    }
}
