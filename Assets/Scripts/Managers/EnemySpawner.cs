using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject player;
    public GravityAttractor gravityAttractor;
    public GameObject enemyA;
    public GameObject enemyB;
    public float spawnInterval;
    public float minSpawnInterval;
    public float spawnIntervalDecreaseRate;

    private float _nextSpawTime = 0f;
    private GameObject _instance;

    private void Update()
    {
        if (Time.time >= _nextSpawTime)
        {
            _nextSpawTime = Time.time + spawnInterval;

            _instance = MyObjectPool.GetInstance(enemyA, MyObjectPool.enemyA);
            _instance.GetComponent<GravityBody>().gravityAttractor = gravityAttractor;
            _instance.transform.parent = transform;

            // spawn on oposite side of the planet from the player
            _instance.transform.position = (player.transform.position * -1).normalized * Planet.currentRadius;

            if (spawnInterval > minSpawnInterval)
                spawnInterval -= spawnIntervalDecreaseRate;
        }
    }

    // Vector3 CalculatePositionInRing(int positionID, int numberOfPlayers)
    // {
    //     // public Transform spawnRingCenter;

    //     if (numberOfPlayers == 1)
    //         return spawnRingCenter.position;

    //     float angle = (positionID) * Mathf.PI * 2 / numberOfPlayers;
    //     float x = Mathf.Cos(angle) * spawnRingRadius;
    //     float z = Mathf.Sin(angle) * spawnRingRadius;
    //     return spawnRingCenter.position + new Vector3(x, 0, z);
    // }
}