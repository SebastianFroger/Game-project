using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPoint : MonoBehaviour
{
    public PointSO pointSO;
    public int pointValue;

    public void Drop()
    {
        pointSO = Instantiate(pointSO);
        var inst = MyObjectPool.Instance.GetInstance(pointSO.prefab, transform.position, transform.rotation);
        pointSO.value = pointValue;
        inst.transform.position = transform.position;
        inst.transform.rotation = transform.rotation;
        inst.transform.localScale = pointValue * Vector3.one;
    }
}
