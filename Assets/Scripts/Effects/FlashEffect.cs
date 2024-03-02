using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    [ExecuteAlways]
    public class FlashEffect : MonoBehaviour
    {
        public TakeDammageEffect flashEffect;

        private Material[] _orgMaterials;
        private MeshRenderer[] _renderers;

        // Start is called before the first frame update
        void Start()
        {
            _renderers = GetComponentsInChildren<MeshRenderer>();
            _orgMaterials = new Material[_renderers.Length];

            for (int i = 0; i < _renderers.Length; i++)
            {
                _orgMaterials[i] = _renderers[i].sharedMaterial;
            }
        }

        // Update is called once per frame
        public void PlayEffect()
        {
            StartCoroutine(flashEffect.Apply(_renderers, _orgMaterials));
        }
    }
}
