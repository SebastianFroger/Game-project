using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyObjectPool : Singleton<MyObjectPool>
{
    private Dictionary<string, List<GameObject>> _poolDict = new();
    private GameObject newInst;

    public GameObject GetInstance(GameObject obj, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
    {
        foreach (var key in _poolDict.Keys)
        {
            if (obj.name == key)
            {
                foreach (var item in _poolDict[key])
                {
                    // select disabled instance
                    if (!item.activeInHierarchy)
                    {
                        item.transform.position = position;
                        item.transform.rotation = rotation;
                        item.SetActive(true);
                        return item;
                    }

                    // create new instance if none are disabled
                    newInst = InstantiateNew(obj);
                    newInst.transform.position = position;
                    newInst.transform.rotation = rotation;
                    _poolDict[key].Add(newInst);
                    newInst.SetActive(true);
                    return newInst;
                }
            }
        }

        // create new list if items is not in pool
        newInst = InstantiateNew(obj);
        _poolDict.Add(obj.name, new List<GameObject>() { newInst });

        newInst.transform.position = position;
        newInst.transform.rotation = rotation;

        return newInst;
    }


    private GameObject InstantiateNew(GameObject obj)
    {
        var inst = Instantiate(obj, this.transform);
        inst.SetActive(false);
        return inst;
    }

    public void Release(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void Release(GameObject obj, float timeDelay)
    {
        StartCoroutine(ReleaseTimeCR(obj, Time.time + timeDelay));
    }

    IEnumerator ReleaseTimeCR(GameObject obj, float time)
    {
        while (Time.time < time)
        {
            yield return null;
        }

        obj.SetActive(false);
    }

    public void ReleaseAll()
    {
        foreach (var list in _poolDict.Values)
        {
            foreach (var item in list)
            {
                item.SetActive(false);
            }
        }
    }

    public void Destroy(GameObject obj)
    {
        foreach (var key in _poolDict.Keys)
        {
            if (obj.name == key)
            {
                _poolDict.Remove(key);
            }
        }
    }

    public void DestroyAll(GameObject obj)
    {
        foreach (var key in _poolDict.Keys)
        {
            _poolDict.Remove(key);
        }
    }
}