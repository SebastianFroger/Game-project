using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnvironmentItem
{
    public GameObject[] prefab;
    public float yGblPosSubtract = 0.2f;
    [Range(1, 10)]
    public float scaleFactorMin;
    [Range(1, 10)]
    public float scaleFactorMax;
    public int amount;
}

public class EnvironmentSpawner : MonoBehaviour
{
    public GameObject shopPrefab;
    public EnvironmentItem[] items;
    public LayerMask myLayerMask;

    private bool _objectPlaced;

    void Start()
    {
        foreach (var item in items)
        {
            Instantiate(item);
        }
    }

    private void Instantiate(EnvironmentItem item)
    {
        var count = 0;

        while (count < item.amount)
        {
            // inst obj
            var randomItem = Random.Range(0, items.Length - 1);
            var inst = Instantiate(item.prefab[randomItem]);

            // set position on top of planet
            inst.transform.position = new Vector3(0, Planet.Instance.GetRadius() - item.yGblPosSubtract, 0);

            // set random y rot
            inst.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(-180f, 180f), 0));

            // move to random position on sphere and rotate
            inst.transform.position = Random.rotation * inst.transform.position;
            inst.transform.LookAt(Vector3.zero);
            inst.transform.Rotate(new Vector3(-90, 0, 0));

            // scale
            var scaleVector = Random.Range(item.scaleFactorMin, item.scaleFactorMax);
            inst.transform.localScale = new Vector3(scaleVector, scaleVector / 2, scaleVector);

            // parent to sphere
            inst.transform.parent = transform;

            count += 1;
        }
    }

    private void FixedUpdate()
    {
        if (!_objectPlaced)
        {
            InstantiateShop();
            MovePlayer();
        }

    }

    void InstantiateShop()
    {
        var randomPosOnSphere = Random.rotation * new Vector3(0, Planet.Instance.GetRadius() - 0.2f, 0);

        Physics.SyncTransforms();
        var collisions = Physics.OverlapSphere(randomPosOnSphere, 5f, myLayerMask);
        while (collisions.Length > 0)
        {
            randomPosOnSphere = Random.rotation * new Vector3(0, Planet.Instance.GetRadius(), 0);
            collisions = Physics.OverlapSphere(randomPosOnSphere, 5f, myLayerMask);
        }

        var inst = Instantiate(shopPrefab);
        inst.transform.position = randomPosOnSphere;
        inst.transform.LookAt(Vector3.zero);
        inst.transform.Rotate(new Vector3(-90, 0, 0));
        inst.transform.parent = transform;

        _objectPlaced = true;
    }

    void MovePlayer()
    {
        var randomPosOnSphere = Random.rotation * new Vector3(0, Planet.Instance.GetRadius(), 0);

        Physics.SyncTransforms();
        var collisions = Physics.OverlapSphere(randomPosOnSphere, 5f, myLayerMask);
        while (collisions.Length > 0)
        {
            randomPosOnSphere = Random.rotation * new Vector3(0, Planet.Instance.GetRadius(), 0);
            collisions = Physics.OverlapSphere(randomPosOnSphere, 5f, myLayerMask);
        }

        var player = GlobalObjectsManager.Instance.player;
        player.transform.position = randomPosOnSphere;
        player.transform.LookAt(Vector3.zero);
        player.transform.Rotate(new Vector3(-90, 0, 0));
        player.transform.parent = transform;

        _objectPlaced = true;
    }
}
