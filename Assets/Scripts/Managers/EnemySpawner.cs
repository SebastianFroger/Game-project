using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : Singleton<EnemySpawner>
{
    public GameObject player;

    private float _spawnInterval;
    private float _minSpawnInterval;
    private float _spawnIntervalDecreaseRate;

    private float _nextSpawTime = 0f;
    private GameObject _instance;
    private RoundDataSO _roundDataSO;

    public void SetRoundData(RoundDataSO roundDataSO)
    {
        _roundDataSO = roundDataSO;
        _spawnInterval = (1 / _roundDataSO.roundDatas[_roundDataSO.currentRound].spawnPrSec);
        _minSpawnInterval = (1 / _roundDataSO.roundDatas[_roundDataSO.currentRound].maxspawnPrSec);
        _nextSpawTime = _nextSpawTime = Time.time + _spawnInterval;
    }

    private void Update()
    {
        // if (!GameManager.Instance.gameStarted) return;
        if (_nextSpawTime == 0f) return;

        if (Time.time > _nextSpawTime)
        {
            _nextSpawTime = Time.time + _spawnInterval;
            _instance = MyObjectPool.Instance.GetInstance(EnemySelector());

            // spawn on oposite side of the planet from the player
            var pos = (player.transform.position * -1).normalized * Planet.Instance.GetRadius();
            _instance.transform.position = pos;

            if (_spawnInterval > _minSpawnInterval)
                _spawnInterval -= _spawnIntervalDecreaseRate;
        }
    }

    private GameObject EnemySelector()
    {
        var randomNr = Random.Range(1, 10);
        var enemyList = _roundDataSO.roundDatas[_roundDataSO.currentRound].enemies;
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