using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlayerRange : MonoBehaviour
{
    public string compareTag = "Player";
    public TransformRuntimeSet enemiesInRange;

    private void OnDisable()
    {
        enemiesInRange.Remove(transform);
    }
}
