using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MyObjectPool : MonoBehaviour
{
    public static GameObject objToCreate;

    public static ObjectPool<GameObject> enemyA;
    public static ObjectPool<GameObject> enemyB;

    // add pickable items pools

    void Start()
    {
        enemyA = new ObjectPool<GameObject>(Create, Get, Release, Destroy, false, 50, 200);
        enemyB = new ObjectPool<GameObject>(Create, Get, Release, Destroy, false, 50, 200);
    }

    GameObject Create()
    {
        return Instantiate(objToCreate);
    }

    void Get(GameObject obj)
    {
        obj.SetActive(true);
    }

    void Release(GameObject obj)
    {
        obj.SetActive(false);
    }

    void Destroy(GameObject obj)
    {
        Destroy(obj);
    }
}
