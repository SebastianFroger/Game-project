using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : Singleton<EnemySpawner>
{
    public RoundDataSO roundDataSO;
    public GameObject player;
    public float spawnRandomness = 0.1f;
    public float spawnHeight;

    private float _spawnInterval;
    private float _spawnIntervalDecreaseRate;
    private float _nextSpawTime = 0f;
    private GameObject _instance;

    private void Update()
    {
        if (_nextSpawTime == 0f)
        {
            _nextSpawTime = Time.time + (1 / roundDataSO.roundDatas[roundDataSO.currentRound].spawnPrSec);
        }

        if (Time.time > _nextSpawTime)
        {
            _nextSpawTime = Time.time + (1 / roundDataSO.roundDatas[roundDataSO.currentRound].spawnPrSec);
            _instance = MyObjectPool.Instance.GetInstance(EnemySelector());

            // spawn on oposite side of the planet from the player
            _instance.transform.position = RandomPoint() * spawnHeight;

            if (_spawnInterval > (1 / roundDataSO.roundDatas[roundDataSO.currentRound].maxspawnPrSec))
                _spawnInterval -= _spawnIntervalDecreaseRate;
        }
    }

    Vector3 RandomPoint()
    {
        var randomPoint = new Vector3(Random.Range(-spawnRandomness, spawnRandomness), Random.Range(-spawnRandomness, spawnRandomness), Random.Range(-spawnRandomness, spawnRandomness));
        return ((player.transform.position * -1) + randomPoint).normalized;
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