using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrystalRock : MonoBehaviour, IHealth
{
    public float hitPoints;
    public GameObject crystalPrefab;

    public delegate void MyDelegate(Transform enemy);
    public static MyDelegate OnDeathEventDelegate;

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
            OnDeathEventDelegate?.Invoke(transform);
            MyObjectPool.Instance.GetInstance(crystalPrefab, transform.position, Quaternion.identity);
            MyObjectPool.Instance.Release(gameObject);
        }
    }
}
