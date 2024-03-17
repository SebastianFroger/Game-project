using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : Singleton<EnemySpawner>
{
    public RoundDataSO roundDataSO;
    public GameObject player;

    private float _spawnInterval;
    private float _spawnIntervalDecreaseRate;
    private float _nextSpawTime = 0f;
    private GameObject _instance;

    private void Update()
    {
        // if (!GameManager.Instance.gameStarted) return;
        if (_nextSpawTime == 0f)
        {
            _nextSpawTime = Time.time + (1 / roundDataSO.roundDatas[roundDataSO.currentRound].spawnPrSec);
        }

        if (Time.time > _nextSpawTime)
        {
            _nextSpawTime = Time.time + (1 / roundDataSO.roundDatas[roundDataSO.currentRound].spawnPrSec);
            _instance = MyObjectPool.Instance.GetInstance(EnemySelector());

            // spawn on oposite side of the planet from the player
            var pos = (player.transform.position * -1).normalized * Planet.Instance.GetRadius();
            _instance.transform.position = pos;

            if (_spawnInterval > (1 / roundDataSO.roundDatas[roundDataSO.currentRound].maxspawnPrSec))
                _spawnInterval -= _spawnIntervalDecreaseRate;
        }
    }

    private GameObject EnemySelector()
    {
        var randomNr = Random.Range(1, 100);
        var enemyList = roundDataSO.roundDatas[roundDataSO.currentRound].enemies;
        var enemy = enemyList[0];

        foreach (var item in enemyList)
        {
            if (randomNr <= item.spawnChance && randomNr < enemy.spawnChance)
                enemy = item;
        }

        return enemy.prefab;
    }

    // Vector3 CalculatePositionInRing(int positionID, Vector3 spawnRingPos)
    // {
    //     var spawnRingRadius = 1;
    //     float angle = (positionID) * Mathf.PI * 2 / 10;
    //     float x = Mathf.Cos(angle) * spawnRingRadius;
    //     float z = Mathf.Sin(angle) * spawnRingRadius;
    //     return spawnRingPos + new Vector3(x, 0, z);
    // }
}