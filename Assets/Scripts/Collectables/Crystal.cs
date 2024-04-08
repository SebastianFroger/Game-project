using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Crystal : MonoBehaviour
{
    public UnitStatsSO unitStats;
    public float crystalPoints;

    private void OnTriggerEnter(Collider other)
    {
        unitStats.crystals += crystalPoints;
        MyObjectPool.Instance.Release(gameObject);
    }
}
