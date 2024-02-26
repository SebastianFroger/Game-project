using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyA;
    public GameObject enemyB;
    public float spawnInterval;

    private float _nextSpawTime = 0f;
    private GameObject _instance;

    private void Update()
    {
        if (Time.time >= _nextSpawTime)
        {
            _nextSpawTime = Time.time + spawnInterval;

            _instance = MyObjectPool.GetInstance(enemyA, MyObjectPool.enemyA);
            _instance.transform.parent = transform;
            // spawn on oposite side of the planet from the player
            _instance.transform.position = (ObjectManager.player.transform.position * -1).normalized * Planet.currentRadius;
        }
    }
}
