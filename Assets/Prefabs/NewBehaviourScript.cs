using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody rb;

    private void Start()
    {
        var colliders = Physics.OverlapSphere(transform.position, 10);

        foreach (var item in colliders)
        {
            if (item.transform == transform) continue;
            item.GetComponent<Rigidbody>().AddExplosionForce(1000, transform.position, 10);
        }
        Debug.Log("ForcePush Execute");
    }
}
