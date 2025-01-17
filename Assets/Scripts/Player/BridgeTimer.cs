using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class BridgeTimer : MonoBehaviour
{
    public float lifeTime = 10f;

    public delegate void OnBridgeDestroyed();
    public static event OnBridgeDestroyed onBridgeDestroyed;


    float destroyTime;

    private void OnEnable()
    {
        destroyTime = 0;
    }

    public void Place()
    {
        destroyTime = Time.time + lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyTime > 0 && Time.time > destroyTime)
        {
            MyObjectPool.Instance.Release(gameObject);
            GlobalObjectsManager.Instance.navMeshSurface.BuildNavMesh();
            onBridgeDestroyed?.Invoke();
        }
    }
}
