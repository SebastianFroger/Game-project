using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PickUpSO : ScriptableObject
{
    public string text;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("PickUpSO says " + text);
    }
}
