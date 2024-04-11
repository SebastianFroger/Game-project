using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Vector3 rotOffset = new Vector3(0, 0, 0);
    public Vector3 posOffset = new Vector3(0, 0, -10);
    public float distance = 65;
    public float backDistance = 10;
    public float slerpValue = .1f;
    public LayerMask layerMask;


    private Vector3 from;
    private Vector3 direction;
    private GameObject player;


    private void Start()
    {
        player = GlobalObjectsManager.Instance.player;
        distance = transform.position.y;
    }

    void Update()
    {
        var position = player.transform.position.normalized * distance;
        position -= player.transform.forward * backDistance;
        transform.position = Vector3.Lerp(transform.position, position, slerpValue);
        transform.LookAt(player.transform.position, player.transform.forward); // Look
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(from, direction);
        Gizmos.DrawSphere(from, 5);
    }
}
