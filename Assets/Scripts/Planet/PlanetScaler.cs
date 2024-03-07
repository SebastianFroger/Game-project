using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlanetScaler : MonoBehaviour
{
    public RuntimeObjectsSO runtimeObjectsSO;
    public int scaleAmount;

    private void OnTriggerEnter(Collider other)
    {
        runtimeObjectsSO.planetInst.GetComponent<Planet>().Grow(scaleAmount);
        MyObjectPool.planetScaler.Release(gameObject);
    }
}
