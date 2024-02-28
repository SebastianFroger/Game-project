using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TakeDammageEffect : ScriptableObject
{
    public abstract IEnumerator Apply(GameObject go);
}
