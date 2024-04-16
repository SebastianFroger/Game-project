using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SeeThrough : MonoBehaviour
{
    public LayerMask seeThroughLayers;
    public float radius = 5f;


    private void OnTriggerStay(Collider other)
    {
        if ((seeThroughLayers & (1 << other.gameObject.layer)) != 0)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<SwapMaterial>()?.SwapToTransparent();
                return;
            }

            // yup
            foreach (var renderer in other.gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((seeThroughLayers & (1 << other.gameObject.layer)) != 0)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<SwapMaterial>()?.SwapToStandard();
                return;
            }

            // nope
            foreach (var renderer in other.gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = true;
            }
        }
    }
}