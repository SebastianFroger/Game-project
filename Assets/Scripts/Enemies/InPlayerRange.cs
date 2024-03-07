using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlayerRange : MonoBehaviour
{
    public EnemiesInRange enemiesInRange;

    private void OnTriggerEnter(Collider other)
    {
        enemiesInRange.Add(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        enemiesInRange.Remove(transform);
    }

    private void OnDisable()
    {
        enemiesInRange.Remove(transform);
    }
}
