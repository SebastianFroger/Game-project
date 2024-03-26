using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float force;
    public float size;
    private bool exploded;

    private void Update()
    {
        if (Time.time > 2 || exploded) return;
        exploded = true;

        DebugExt.Log(this, $"explode");
        var colliders = Physics.OverlapSphere(transform.position, size);
        foreach (var item in colliders)
        {
            item.GetComponent<Rigidbody>().AddForce((item.transform.position - transform.position).normalized * force, ForceMode.Impulse);
        }
    }
}

