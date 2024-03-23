using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPoint : MonoBehaviour
{
    public PointSO pointSO;

    public void Drop()
    {
        var inst = MyObjectPool.Instance.GetInstance(pointSO.prefab, transform.position, transform.rotation);
        inst.transform.position = transform.position;
        inst.transform.rotation = transform.rotation;
    }
}
