using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnvironmentItem
{
    public GameObject[] prefab;
    public float yGblPosSubtract = 0.2f;

    [Header("Single items placed randomly")]
    public int amount;

    [Header("Clumps of item")]
    [Range(1, 30)]
    public int amountOfClumps;
    [Range(1, 30)]
    public int itemsPrClump;
    [Range(1, 30)]
    public float minDistance = 2;
    [Range(1, 30)]
    public float maxDistance = 5;

    [Header("Scaling")]
    [Range(1, 10)]
    public float scaleFactorMin;
    [Range(1, 10)]
    public float scaleFactorMax;
}

public class EnvironmentSpawner : MonoBehaviour
{
    public GameObject shopPrefab;
    public EnvironmentItem[] items;
    public LayerMask myLayerMask;



    private bool _creatingMap;
    private Quaternion _addedRotations = Quaternion.identity;

    void Start()
    {
        foreach (var item in items)
        {
            Instantiate(item);
        }
    }

    void Instantiate(EnvironmentItem item)
    {
        _creatingMap = true;

        // clumps
        for (int i = 0; i < item.amountOfClumps; i++)
        {
            _addedRotations = UnityEngine.Random.rotation;

            for (int j = 0; j < item.itemsPrClump; j++)
            {
                // inst obj
                var randomItem = UnityEngine.Random.Range(0, items.Length - 1);
                var inst = Instantiate(item.prefab[randomItem]);

                // set random y rot
                inst.transform.rotation = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(-180f, 180f), 0));

                var randDist = UnityEngine.Random.Range(item.minDistance, item.maxDistance);
                var rotAdjusment = Quaternion.Euler(new Vector3(UnityEngine.Random.Range(-randDist, randDist), UnityEngine.Random.Range(-randDist, randDist), UnityEngine.Random.Range(-randDist, randDist)));
                inst.transform.position = _addedRotations * new Vector3(0, Planet.Instance.GetRadius() - item.yGblPosSubtract, 0);
                _addedRotations *= rotAdjusment;

                // rotate to sphere
                inst.transform.LookAt(Vector3.zero);
                inst.transform.Rotate(new Vector3(-90, 0, 0));

                // scale
                var scaleVector = UnityEngine.Random.Range(item.scaleFactorMin, item.scaleFactorMax);
                inst.transform.localScale = new Vector3(scaleVector, scaleVector / 2, scaleVector);

                // parent to sphere
                inst.transform.parent = transform;
            }
        }


        // total random placment
        for (int i = 0; i < item.amount; i++)
        {
            // inst obj
            var randomItem = UnityEngine.Random.Range(0, items.Length - 1);
            var inst = Instantiate(item.prefab[randomItem]);

            // set position on top of planet
            inst.transform.position = new Vector3(0, Planet.Instance.GetRadius() - item.yGblPosSubtract, 0);

            // set random y rot
            inst.transform.rotation = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(-180f, 180f), 0));

            // move to random position on sphere and rotate
            inst.transform.position = UnityEngine.Random.rotation * inst.transform.position;
            inst.transform.LookAt(Vector3.zero);
            inst.transform.Rotate(new Vector3(-90, 0, 0));

            // scale
            var scaleVector = UnityEngine.Random.Range(item.scaleFactorMin, item.scaleFactorMax);
            inst.transform.localScale = new Vector3(scaleVector, scaleVector / 2, scaleVector);

            // parent to sphere
            inst.transform.parent = transform;
        }
    }

    void InstantiateRandomRock(EnvironmentItem item)
    {
        // inst obj
        var randomItem = UnityEngine.Random.Range(0, items.Length - 1);
        var inst = Instantiate(item.prefab[randomItem]);

        // set position on top of planet
        inst.transform.position = new Vector3(0, Planet.Instance.GetRadius() - item.yGblPosSubtract, 0);

        // set random y rot
        inst.transform.rotation = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(-180f, 180f), 0));

        // move to random position on sphere and rotate
        inst.transform.position = UnityEngine.Random.rotation * inst.transform.position;
        inst.transform.LookAt(Vector3.zero);
        inst.transform.Rotate(new Vector3(-90, 0, 0));

        // scale
        var scaleVector = UnityEngine.Random.Range(item.scaleFactorMin, item.scaleFactorMax);
        inst.transform.localScale = new Vector3(scaleVector, scaleVector / 2, scaleVector);

        // parent to sphere
        inst.transform.parent = transform;
    }


    private void FixedUpdate()
    {
        if (_creatingMap)
        {
            InstantiateShop();
            MovePlayer();

            _creatingMap = false;
        }

    }

    void InstantiateShop()
    {
        var randomPosOnSphere = UnityEngine.Random.rotation * new Vector3(0, Planet.Instance.GetRadius() - 0.2f, 0);

        Physics.SyncTransforms();
        var collisions = Physics.OverlapSphere(randomPosOnSphere, 5f, myLayerMask);
        while (collisions.Length > 0)
        {
            randomPosOnSphere = UnityEngine.Random.rotation * new Vector3(0, Planet.Instance.GetRadius(), 0);
            collisions = Physics.OverlapSphere(randomPosOnSphere, 5f, myLayerMask);
        }

        var inst = Instantiate(shopPrefab);
        inst.transform.position = randomPosOnSphere;
        inst.transform.LookAt(Vector3.zero);
        inst.transform.Rotate(new Vector3(-90, 0, 0));
        inst.transform.parent = transform;

        _creatingMap = true;
    }

    void MovePlayer()
    {
        var randomPosOnSphere = UnityEngine.Random.rotation * new Vector3(0, Planet.Instance.GetRadius(), 0);

        Physics.SyncTransforms();
        var collisions = Physics.OverlapSphere(randomPosOnSphere, 5f, myLayerMask);
        while (collisions.Length > 0)
        {
            randomPosOnSphere = UnityEngine.Random.rotation * new Vector3(0, Planet.Instance.GetRadius(), 0);
            collisions = Physics.OverlapSphere(randomPosOnSphere, 5f, myLayerMask);
        }

        var player = GlobalObjectsManager.Instance.player;
        player.transform.position = randomPosOnSphere;
        player.transform.LookAt(Vector3.zero);
        player.transform.Rotate(new Vector3(-90, 0, 0));
        player.transform.parent = transform;

        _creatingMap = true;
    }
}
