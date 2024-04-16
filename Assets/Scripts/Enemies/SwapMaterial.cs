using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMaterial : MonoBehaviour
{
    public Material transparentMaterial;

    Material standardMaterial;
    MeshRenderer[] renderers;

    private void Start()
    {
        standardMaterial = GetComponentInChildren<MeshRenderer>().material;
        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void SwapToTransparent()
    {
        foreach (var renderer in renderers)
        {
            renderer.material = transparentMaterial;
        }
    }

    public void SwapToStandard()
    {
        foreach (var renderer in renderers)
        {
            renderer.material = standardMaterial;
        }
    }
}
