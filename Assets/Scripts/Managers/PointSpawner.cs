using System.Collections;
using System.Collections.Generic;
using Shooter;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    public GameObject pointPrefab;

    public void SpawnPoint()
    {
        Debug.Log("Spawn Point");
        // var instance = MyObjectPool.points.Get();
        // instance.transform.position = transform.position;
        // instance.transform.rotation = transform.rotation;
    }
}
