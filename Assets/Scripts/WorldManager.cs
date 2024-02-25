using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static GameObject player;
    public static GameObject planet;
    public static GravityAttractor attractor;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        planet = GameObject.FindWithTag("Planet");
        attractor = planet.GetComponent<GravityAttractor>();
    }
}
