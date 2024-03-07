using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AddToGlobalManager : MonoBehaviour
{
    public GameObject player;
    public GameObject planet;

    void Awake()
    {
        switch (gameObject.name)
        {
            case "Player":
                player = gameObject;
                break;

                // case "Planet":
                //     planet = gameObject;
                //     attractor = gameObject.GetComponent<GravityAttractor>();
                //     break;

        }
    }
}
