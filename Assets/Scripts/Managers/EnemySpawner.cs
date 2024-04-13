using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : Singleton<EnemySpawner>
{
    public RoundDataSO roundDataSO;
    public float mapSizeX = 10f;
    public float mapSizeZ = 10f;
    public LayerMask groundLayer;

    private float _nextSpawTime = 0f;


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
            if (Physics.Raycast(randomPoint, Vector3.down, out RaycastHit hit2, 1000f, groundLayer))
            {
                raycastHit = hit2.point;
                inCameraView = IsInCameraView(raycastHit);
            }
        }

        return raycastHit;
    }

    Vector3 GetRandomPoint()
    {
        return new Vector3(Random.Range(-mapSizeX, mapSizeX), 100, Random.Range(-mapSizeZ, mapSizeZ));
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