using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class MyObjectPool : Singleton<MyObjectPool>
{
    private Dictionary<string, List<GameObject>> _poolDict = new();
    private GameObject newInst;
    private List<GameObject> newList;

    public GameObject GetInstance(GameObject obj)
    {
        foreach (var key in _poolDict.Keys)
        {
            if (obj.name == key)
            {
                foreach (var item in _poolDict[key])
                {
                    if (!item.activeInHierarchy)
                    {
                        item.SetActive(true);
                        return item;
                    }

                    newInst = Instantiate(obj);
                    newInst.transform.parent = transform;
                    _poolDict[key].Add(newInst);
                    return newInst;
                }
            }
        }

        newInst = Instantiate(obj);
        newInst.transform.parent = transform;
        newList = new List<GameObject>() { newInst };
        _poolDict.Add(obj.name, newList);
        return newInst;
    }

    public void Release(GameObject obj)
    {
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