using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class BridgeTimer : MonoBehaviour
{
    public float lifeTime = 10f;

    float destroyTime;

    private void OnEnable()
    {
        destroyTime = Time.time + lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > destroyTime)
        {
            MyObjectPool.Instance.Release(gameObject);
            GlobalObjectsManager.Instance.navMeshSurface.BuildNavMesh();
        }
    }
}
