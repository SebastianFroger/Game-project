using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TakeDammageEffect : ScriptableObject
{
    [ExecuteAlways]
    public abstract IEnumerator Apply(MeshRenderer[] renderers, Material[] orgMaterials);
}
