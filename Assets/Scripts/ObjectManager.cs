using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject player;
    public static GameObject playerObj;
    public GameObject planet;
    public static GameObject planetObj;
    public static GravityAttractor attractor;

    private void Start()
    {
        playerObj = player;
        planetObj = planet;
        attractor = planet.GetComponent<GravityAttractor>();
    }
}
