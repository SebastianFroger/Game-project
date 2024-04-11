using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : Singleton<EnemySpawner>
{
    public RoundDataSO roundDataSO;
    public float mapSizeX = 10f;
    public float mapSizeZ = 10f;

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
        var randomPoint = new Vector3(Random.Range(-mapSizeX, mapSizeX), 0, Random.Range(-mapSizeZ, mapSizeZ));
        Vector3 camViewPos = Camera.main.WorldToViewportPoint(randomPoint);
        while (camViewPos.x > 0 && camViewPos.x < 1 && camViewPos.y > 0 && camViewPos.y < 1)
        {
            randomPoint = new Vector3(Random.Range(-mapSizeX, mapSizeX), 0, Random.Range(-mapSizeZ, mapSizeZ));
            camViewPos = Camera.main.WorldToViewportPoint(randomPoint);
        }
        return randomPoint;
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