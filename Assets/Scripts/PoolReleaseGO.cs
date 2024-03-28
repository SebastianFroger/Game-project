using System.Collections;
using System.Collections.Generic;
using Shooter;
using UnityEngine;

public class PoolReleaseGO : MonoBehaviour
{
    public void Release()
    {

        MyObjectPool.Instance.Release(gameObject);
    }
}
