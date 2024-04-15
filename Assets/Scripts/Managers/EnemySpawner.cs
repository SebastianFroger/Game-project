using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class EnemySpawner : Singleton<EnemySpawner>
{
    public RoundDataSO roundDataSO;
    public LayerMask groundLayer;

    private float _nextSpawTime = 0f;
    float topPlatformHeight = 0f;
    GameObject lvlRoot;
    Bounds bounds;


    private void Start()
    {
        lvlRoot = GlobalObjectsManager.Instance.navMeshSurface.gameObject;
        bounds = lvlRoot.GetComponent<NavMeshSurface>().navMeshData.sourceBounds;

        // loop through all the platforms and find the highest one
        for (int i = 0; i < lvlRoot.transform.childCount; i++)
        {
            var child = lvlRoot.transform.GetChild(i);
            if (child.position.y > topPlatformHeight)
            {
                topPlatformHeight = child.position.y;
            }
        }
    }

    private void Update()
    {
        if (_nextSpawTime == 0f)
        {
            _nextSpawTime = Time.time + (1 / roundDataSO.roundDatas[roundDataSO.currentRound].spawnPrSec);
        }

        if (Time.time > _nextSpawTime)
        {
            _nextSpawTime = Time.time + (1 / roundDataSO.roundDatas[roundDataSO.currentRound].spawnPrSec);
            MyObjectPool.Instance.GetInstance(EnemySelector(), SpawnPoint(), Quaternion.identity);
        }
    }

    private Vector3 SpawnPoint()
    {
        Vector3 raycastHit = Vector3.zero;
        var randomPoint = new Vector3();
        var inCameraView = true;

        while (raycastHit == Vector3.zero || inCameraView)
        {
            randomPoint = GetRandomPoint();
            if (Physics.Raycast(randomPoint, Vector3.down, out RaycastHit hit2, 2f, groundLayer))
            {
                raycastHit = hit2.point;
                inCameraView = IsInCameraView(raycastHit);
            }
        }

        return raycastHit;
    }

    Vector3 GetRandomPoint()
    {
        var randomLvl = Random.Range(0, topPlatformHeight / 10) * 10 + 1;
        return new Vector3(Random.Range(bounds.min.x, bounds.max.x), randomLvl, Random.Range(bounds.min.z, bounds.max.z));
    }

    bool IsInCameraView(Vector3 point)
    {
        Vector3 camViewPos = Camera.main.WorldToViewportPoint(point);
        return camViewPos.x > 0 && camViewPos.x < 1 && camViewPos.y > 0 && camViewPos.y < 1;
    }

    private GameObject EnemySelector()
    {
        var enemyList = roundDataSO.roundDatas[roundDataSO.currentRound].enemies;

        var randomNr = Random.Range(0f, 1f);
        var numForAdding = 0f;
        var total = 0f;


        foreach (var item in enemyList)
        {
            total += item.spawnChance;
        }

        foreach (var item in enemyList)
        {
            if (item.spawnChance / total + numForAdding >= randomNr)
            {
                return item.prefab;
            }
            numForAdding += item.spawnChance / total;
        }
        return enemyList[0].prefab;
    }
}