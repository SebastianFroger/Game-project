using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{

    public class PlanetScaler : MonoBehaviour
    {
        public int scaleAmount;
        public GlobalManagerSO globalManagerSO;

        private void OnTriggerEnter(Collider other)
        {
            globalManagerSO.planet.GetComponent<Planet>().Grow(scaleAmount);
            MyObjectPool.planetScaler.Release(gameObject);
            Debug.Log("scale");
        }
    }
}
