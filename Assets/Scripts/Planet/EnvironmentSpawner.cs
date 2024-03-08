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
    public EnvironmentItem[] items;

    // Start is called before the first frame update
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
            var randomItem = Random.Range(0, items.Length - 1);
            var inst = Instantiate(item.prefab[randomItem]);
            inst.transform.position = new Vector3(0, Planet.currentRadius - item.yGblPosSubtract, 0);
            transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));

            var scaleVector = Random.Range(item.scaleFactorMin, item.scaleFactorMax);
            inst.transform.localScale = new Vector3(scaleVector, scaleVector / 2, scaleVector);

            inst.transform.parent = transform;
            count += 1;
        }
    }
}
