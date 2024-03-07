using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class MyObjectPool : Singleton<MyObjectPool>
{

    public ObjectPool<GameObject> enemyA;
    public ObjectPool<GameObject> enemyB;
    public ObjectPool<GameObject> bullet;
    public ObjectPool<GameObject> planetScaler;
    public ObjectPool<GameObject> points;

    private GameObject objToCreate;

    void Start()
    {
        enemyA = new ObjectPool<GameObject>(Create, Get, Release, Destroy, false, 50, 200);
        enemyB = new ObjectPool<GameObject>(Create, Get, Release, Destroy, false, 50, 200);
        bullet = new ObjectPool<GameObject>(Create, Get, Release, Destroy, false, 50, 200);
        planetScaler = new ObjectPool<GameObject>(Create, Get, Release, Destroy, false, 50, 200);
        points = new ObjectPool<GameObject>(Create, Get, Release, Destroy, false, 50, 200);
    }

    public GameObject GetInstance(GameObject obj, ObjectPool<GameObject> pool)
    {
        objToCreate = obj;
        var inst = pool.Get();
        if (inst.transform.parent != transform)
            inst.transform.parent = transform;
        return inst;
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