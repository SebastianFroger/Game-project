using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Shooter;
using UnityEngine;

public class PlanetScalerManager : MonoBehaviour
{
    public GameObject prefab;

    private float _planetStartSize;
    private bool _hasGrown;

    // Start is called before the first frame update
    void Start()
    {
        _planetStartSize = Planet.currentRadius;

        var inst = MyObjectPool.GetInstance(prefab, MyObjectPool.planetScaler);
        var position = new Vector3(Random.Range(1f, -1f), Random.Range(1f, -1f), Random.Range(1f, -1f)).normalized * Planet.currentRadius;
        inst.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Planet.currentRadius > _planetStartSize && !_hasGrown)
            _hasGrown = true;

        if (_hasGrown && _planetStartSize > Planet.currentRadius)
        {
            var inst = MyObjectPool.GetInstance(prefab, MyObjectPool.planetScaler);
            var position = new Vector3(Random.Range(1f, -1f), Random.Range(1f, -1f), Random.Range(1f, -1f)).normalized * Planet.currentRadius;
            inst.transform.position = position;

            _hasGrown = false;
        }
    }
}
