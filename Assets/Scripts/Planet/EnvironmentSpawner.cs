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

    void Start()
    {
        foreach (var item in items)
        {
            Instantiate(item);
        }
        StartCoroutine(InstantiateShop());
    }

    private void Instantiate(EnvironmentItem item)
    {
        var count = 0;

        while (count < item.amount)
        {
            RotatePlanetRandom();

            // instantiate random env object and parent to planet
            var randomItem = Random.Range(0, items.Length - 1);
            var inst = Instantiate(item.prefab[randomItem]);
            inst.transform.position = new Vector3(0, Planet.Instance.GetRadius() - item.yGblPosSubtract, 0);
            var scaleVector = Random.Range(item.scaleFactorMin, item.scaleFactorMax);
            inst.transform.localScale = new Vector3(scaleVector, scaleVector / 2, scaleVector);

            inst.transform.parent = transform;

            count += 1;
        }
    }

    void RotatePlanetRandom()
    {
        transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));
    }

    IEnumerator InstantiateShop()
    {
        yield return null;

        var shopInst = Instantiate(shopPrefab);
        shopInst.SetActive(false);
        shopInst.transform.position = new Vector3(0, Planet.Instance.GetRadius() - 0.2f, 0);

        RotatePlanetRandom();
        var collisions = Physics.OverlapSphere(shopInst.transform.position, 10f, 1 << 11);
        while (collisions.Length > 0)
        {
            RotatePlanetRandom();
            collisions = Physics.OverlapSphere(shopInst.transform.position, 10f, 1 << 11);
            yield return null;
        }


        // // find position
        // var center = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Planet.Instance.GetRadius();
        // var collisions = Physics.OverlapSphere(center, 5f, 1 << 11);
        // while (collisions.Length > 0)
        // {
        //     center = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Planet.Instance.GetRadius();
        //     collisions = Physics.OverlapSphere(center, 5f, 1 << 11);
        //     yield return null;
        // }

        shopInst.transform.parent = transform;
        shopInst.SetActive(true);
        RotatePlanetRandom();
    }
}
