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
    private GameObject _prefab;
    private GameObject _instance;

    public void SetRoundData(RoundDataSO roundDataSO)
    {
        _spawnInterval = (1 / roundDataSO.roundDatas[roundDataSO.currentRound].spawnPrSec);
        _minSpawnInterval = (1 / roundDataSO.roundDatas[roundDataSO.currentRound].maxspawnPrSec);
        _prefab = roundDataSO.roundDatas[roundDataSO.currentRound].enemies[0].prefab;
        _nextSpawTime = _nextSpawTime = Time.time + _spawnInterval;
    }

    private void Update()
    {
        if (_nextSpawTime == 0f) return;

        if (Time.time >= _nextSpawTime)
        {
            _nextSpawTime = Time.time + _spawnInterval;
            _instance = MyObjectPool.Instance.GetInstance(_prefab, MyObjectPool.Instance.enemyA);

            // spawn on oposite side of the planet from the player
            var pos = (player.transform.position * -1).normalized * Planet.currentRadius;
            _instance.transform.position = pos;

            if (_spawnInterval > _minSpawnInterval)
                _spawnInterval -= _spawnIntervalDecreaseRate;
        }
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