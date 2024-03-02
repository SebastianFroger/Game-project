using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace Shooter
{
    public class MyObjectPool : MonoBehaviour
    {

        public static ObjectPool<GameObject> enemyA;
        public static ObjectPool<GameObject> enemyB;
        public static ObjectPool<GameObject> bullet;
        public static ObjectPool<GameObject> planetScaler;
        public static ObjectPool<GameObject> points;

        private static GameObject objToCreate;

        void Start()
        {
            enemyA = new ObjectPool<GameObject>(Create, Get, Release, Destroy, false, 50, 200);
            enemyB = new ObjectPool<GameObject>(Create, Get, Release, Destroy, false, 50, 200);
            bullet = new ObjectPool<GameObject>(Create, Get, Release, Destroy, false, 50, 200);
            planetScaler = new ObjectPool<GameObject>(Create, Get, Release, Destroy, false, 50, 200);
            points = new ObjectPool<GameObject>(Create, Get, Release, Destroy, false, 50, 200);
        }

        public static GameObject GetInstance(GameObject obj, ObjectPool<GameObject> pool)
        {
            objToCreate = obj;
            return pool.Get();
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
}