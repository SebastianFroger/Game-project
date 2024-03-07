using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
[CreateAssetMenu(fileName = "FlashEffect", menuName = "ScriptableObjects/Effects/FlashEffect", order = 1)]
public class FlashEffectSO : TakeDammageEffectSO
{
    public Material material;
    public float damageFlashTime = 0.1f;


    public override IEnumerator Apply(MeshRenderer[] renderers, Material[] orgMaterials)
    {

        // switch materials and back
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].sharedMaterial = material;
        }

        yield return new WaitForSeconds(damageFlashTime);

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].sharedMaterial = orgMaterials[i];
        }
    }
}
