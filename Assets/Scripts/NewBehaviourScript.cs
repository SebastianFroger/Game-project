using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public LayerMask myLayerMask;

    private Vector3 randomVector;
    private Vector3 hitPoint;


    private void Start()
    {
        randomVector = UnityEngine.Random.rotation.eulerAngles.normalized * 10;
        // GetRandomPlacement(ref hitPoint);
    }

    private void Update()
    {
        Debug.DrawLine(randomVector, Vector3.zero, Color.red);
    }

    private void FixedUpdate()
    {
        GetRandomPlacement(ref hitPoint);
    }

    Collider[] GetRandomPlacement(ref Vector3 hitPoint)
    {
        Collider[] colliders = new Collider[1];
        if (Physics.Raycast(randomVector, -randomVector, out RaycastHit hit, 1000f))
        {
            DebugExt.Log(this, $"hit {hit.point}");
            hitPoint = hit.point;
            colliders = Physics.OverlapSphere(hit.point, 5f, myLayerMask);
        }

        DebugExt.Log(this, $"randomVector {randomVector}");
        DebugExt.Log(this, $"hitPoint {hitPoint}");

        return colliders;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hitPoint, 1f);
    }
}

