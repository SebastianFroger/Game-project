using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// NOTE! make sure to insert the in scene prefabs
public class ObjectManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public static GameObject player;
    public GameObject planetPrefab;
    public static GameObject planet;
    public static GravityAttractor attractor;
    public static float planetRadius;

    private void Start()
    {
        player = playerPrefab;
        planet = planetPrefab;
        attractor = planetPrefab.GetComponent<GravityAttractor>();
    }
}
