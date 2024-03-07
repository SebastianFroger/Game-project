using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyA;
    public GameObject enemyB;
    public float spawnInterval;
    public float minSpawnInterval;
    public float spawnIntervalDecreaseRate;

    private float _nextSpawTime = 0f;
    private GameObject _instance;

    private void Start()
    {
        _nextSpawTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        if (Time.time >= _nextSpawTime)
        {
            _nextSpawTime = Time.time + spawnInterval;
            _instance = MyObjectPool.Instance.GetInstance(enemyA, MyObjectPool.Instance.enemyA);

            // spawn on oposite side of the planet from the player
            var pos = (player.transform.position * -1).normalized * Planet.currentRadius;
            _instance.transform.position = pos;

            if (spawnInterval > minSpawnInterval)
                spawnInterval -= spawnIntervalDecreaseRate;
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