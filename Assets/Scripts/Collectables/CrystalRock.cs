using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrystalRock : MonoBehaviour, IHealth
{
    public float hitPoints;
    public GameObject crystalPrefab;


    float currentHitPoints;

    private void Start()
    {
        currentHitPoints = hitPoints;
    }

    private void OnEnable()
    {
        currentHitPoints = hitPoints;
    }

    public void TakeDamage(float amount)
    {
        currentHitPoints -= amount;

        if (currentHitPoints <= 0)
        {
            MyObjectPool.Instance.GetInstance(crystalPrefab, transform.position, Quaternion.identity);
            MyObjectPool.Instance.Release(gameObject);
        }
    }
}
