using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TakeDammageEffectSO : ScriptableObject
{
    [ExecuteAlways]
    public abstract IEnumerator Apply(MeshRenderer[] renderers, Material[] orgMaterials);
}
