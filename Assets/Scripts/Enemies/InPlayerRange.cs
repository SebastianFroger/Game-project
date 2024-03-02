using System.Collections;
using System.Collections.Generic;
using Shooter;
using UnityEngine;

public class InPlayerRange : MonoBehaviour
{
    public GameObjectRuntimeSet inRange;

    private void OnTriggerEnter(Collider other)
    {
        inRange.Add(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        inRange.Remove(transform);
    }

    private void OnDisable()
    {
        inRange.Remove(transform);
    }
}
