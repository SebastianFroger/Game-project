using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyA;
    public GameObject enemyB;


    private void Update()
    {
        if (Time.frameCount != 1000) return;
        MyObjectPool.objToCreate = enemyA;
        var go = MyObjectPool.enemyA.Get();
        InitGO(go, Vector3.left * Planet.currentRadius, Quaternion.identity);
    }

    private void InitGO(GameObject obj, Vector3 position, Quaternion rotation)
    {
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.GetComponent<GravityBody>().attractor = ObjectManager.attractor;
        obj.GetComponent<EnemyControl>().player = ObjectManager.player.transform;
    }
}
