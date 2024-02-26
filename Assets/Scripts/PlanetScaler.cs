using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScaler : MonoBehaviour
{
    public int scaleAmount;

    private void OnTriggerEnter(Collider other)
    {
        ObjectManager.planet.GetComponent<Planet>().Grow(scaleAmount);
        MyObjectPool.planetScaler.Release(gameObject);
    }
}
