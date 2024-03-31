using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public UnitStatsSO unitStats;
    public int value = 1;
    public float groundHeightAdjust = 0.5f;

    private void Update()
    {
        transform.position = transform.position.normalized * (Planet.Instance.GetRadius() + groundHeightAdjust);
    }

    private void OnTriggerEnter(Collider other)
    {
        unitStats.points += 1;
        MyObjectPool.Instance.Release(gameObject);
    }
}
