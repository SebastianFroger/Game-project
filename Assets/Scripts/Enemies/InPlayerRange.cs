using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlayerRange : MonoBehaviour
{
    public string compareTag = "Player";
    public EnemiesInRange enemiesInRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(compareTag))
            enemiesInRange.Add(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(compareTag))
            enemiesInRange.Remove(transform);
    }

    private void OnDisable()
    {
        enemiesInRange.Remove(transform);
    }
}
