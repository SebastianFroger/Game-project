using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Point : MonoBehaviour
{
    public PlayerStats playerStats;
    public int value = 1;

    private void OnTriggerEnter(Collider other)
    {
        playerStats.points += 1;
        MyObjectPool.points.Release(gameObject);
    }
}
