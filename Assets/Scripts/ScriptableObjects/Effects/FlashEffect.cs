using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{

    [CreateAssetMenu(fileName = "FlashEffect", menuName = "ScriptableObjects/Effects/FlashEffect", order = 1)]
    public class FlashEffect : TakeDammageEffect
    {
        public Material material;
        public float damageFlashTime = 0.1f;

        private Material[] _orgMaterials;
        private MeshRenderer[] _renderers;

        public void ApplyEffect(GameObject go)
        {
            go.G
            // MonoBehaviour.StartCoroutine("Apply");
        }

        public override IEnumerator Apply(GameObject go)
        {
            Debug.Log("flash effect");
            // Get all materials 
            _renderers = go.GetComponentsInChildren<MeshRenderer>();
            _orgMaterials = new Material[_renderers.Length];

            for (int i = 0; i < _renderers.Length; i++)
            {
                _orgMaterials[i] = _renderers[i].sharedMaterial;
            }

            // switch materials and back
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].sharedMaterial = material;
            }

            yield return new WaitForSeconds(damageFlashTime);

            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].sharedMaterial = _orgMaterials[i];
            }
        }
    }
}
