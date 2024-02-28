using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public PickUpSO pickUpSO;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ItemScript triggreded");
        pickUpSO.OnTriggerEnter(other);
    }
}
