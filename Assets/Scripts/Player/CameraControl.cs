using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Vector3 rotOffset = new Vector3(0, 0, 0);
    public Vector3 posOffset = new Vector3(0, 0, -10);
    public float distance = 65;
    public LayerMask layerMask;


    private Vector3 from;
    private Vector3 direction;
    private GameObject player;
    private GravityBody gravityBody;


    private void Start()
    {
        player = GlobalObjectsManager.Instance.player;
        gravityBody = player.GetComponent<GravityBody>();
        distance = transform.position.y;
    }

    void FixedUpdate()
    {

        // works, but avoid using player trs up. use dir from zero instead


        from = player.transform.position * 2;
        if (gravityBody.isInside)
            from = -player.transform.position * 2;

        direction = player.transform.position - from;
        if (Physics.Raycast(from, direction, out RaycastHit hit, 99999, layerMask))
        {
            transform.position = hit.normal.normalized * distance;

            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal.normalized) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime);


            // Vector3 v = playerTransform.position - Vector3.zero;
            // v += v.normalized * distance;
            // transform.position = Vector3.zero + v;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(from, direction);
        Gizmos.DrawSphere(from, 5);
    }
}
