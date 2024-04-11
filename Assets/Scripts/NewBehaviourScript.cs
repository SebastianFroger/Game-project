using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 5;
    }
}
