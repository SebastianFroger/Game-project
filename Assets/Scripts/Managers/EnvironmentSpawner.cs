using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class EnvironmentSpawner : Singleton<EnvironmentSpawner>
{
    public RoundDataSO roundDataSO;
    public GameObject crystalPrefab;
    public LayerMask groundLayer;

    // float topPlatformHeight = 0f;
    GameObject lvlRoot;
    Bounds bounds;

    public void SpawnEnvironment()
    {
        lvlRoot = GlobalObjectsManager.Instance.navMeshSurface.gameObject;
        bounds = lvlRoot.GetComponent<NavMeshSurface>().navMeshData.sourceBounds;
        SpawnCrystals();
    }

    void SpawnCrystals()
    {
        var amount = (roundDataSO.currentRound + 1);
        for (int i = 0; i < amount; i++)
        {
            MyObjectPool.Instance.GetInstance(crystalPrefab, SpawnPoint(Vector3.up / 2), Quaternion.identity);
        }
    }

    // void GetHighestLevel()
    // {
    //     topPlatformHeight = 0f;
    //     // loop through all the platforms and find the highest one
    //     for (int i = 0; i < lvlRoot.transform.childCount; i++)
    //     {
    //         var child = lvlRoot.transform.GetChild(i);
    //         if (child.position.y > topPlatformHeight)
    //         {
    //             topPlatformHeight = child.position.y;
    //         }
    //     }
    // }

    private Vector3 SpawnPoint(Vector3 offset = new Vector3())
    {
        Vector3 raycastHit = Vector3.zero;
        var randomPoint = new Vector3();
        while (raycastHit == Vector3.zero)
        {
            randomPoint = GetRandomPoint();
            if (Physics.Raycast(randomPoint, Vector3.down, out RaycastHit hit2, 110f, groundLayer))
            {
                raycastHit = hit2.point + offset;
            }
        }

        return raycastHit;
    }

    Vector3 GetRandomPoint()
    {
        // var height = Mathf.Round(Random.Range(0, topPlatformHeight) / 10);
        return new Vector3(Random.Range(bounds.min.x, bounds.max.x), 100, Random.Range(bounds.min.z, bounds.max.z));
    }
}
