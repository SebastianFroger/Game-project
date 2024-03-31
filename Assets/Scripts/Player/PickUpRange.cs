using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRange : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    private SphereCollider pickUpCollider;
    // Start is called before the first frame update
    void Start()
    {
        pickUpCollider = GetComponent<SphereCollider>();
        pickUpCollider.radius = unitStatsSO.pickUpRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickUpCollider.radius != unitStatsSO.pickUpRange)
            pickUpCollider.radius = unitStatsSO.pickUpRange;
    }
}
