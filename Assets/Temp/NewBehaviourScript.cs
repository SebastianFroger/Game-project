using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform lookTarget;
    Rigidbody _rb;
    private Vector3 localDir;
    private Vector3 targetDir;
    Vector3 localDirZeroed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        transform.LookAt(Vector3.zero, transform.forward);
        transform.Rotate(-90, 0, 0);
    }

    private void Update()
    {
        targetDir = GlobalObjectsManager.Instance.player.transform.position - transform.position;
        localDir = transform.InverseTransformPoint(targetDir).normalized;
        localDirZeroed = new Vector3(localDir.x, 0f, localDir.z).normalized;
        lookTarget.localPosition = new Vector3(localDir.x, 0f, localDir.z).normalized;

        // localDir = transform.TransformVector(localDir).normalized;



        // localDir
        // transform.LookAt(localDir, transform.up);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // _rb.AddForce(-transform.up.normalized * 10);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, localDir);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, targetDir);
    }
}
