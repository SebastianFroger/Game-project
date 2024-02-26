using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyA;
    public GameObject enemyB;
    public float spawnInterval;

    private float _nextSpawTime = 0f;

    private void Update()
    {
        if (Time.time >= _nextSpawTime)
        {
            var go = MyObjectPool.GetInstance(enemyA, MyObjectPool.enemyA);
            InitGO(go, Vector3.left * Planet.currentRadius, Quaternion.identity);

            _nextSpawTime = Time.time + spawnInterval;
        }
    }

    private void InitGO(GameObject obj, Vector3 position, Quaternion rotation)
    {
        obj.transform.position = position;
        obj.transform.rotation = rotation;
    }
}
