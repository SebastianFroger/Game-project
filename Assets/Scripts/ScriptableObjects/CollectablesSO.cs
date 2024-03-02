using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectablesSO : ScriptableObject
{
    public abstract void PickUp(GameObject go = null);
}
