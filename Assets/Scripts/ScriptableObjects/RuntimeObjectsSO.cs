using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class RuntimeObjectsSO : ScriptableObject
{
    public GameObject player;
    [HideInInspector] public GameObject playerInst;
    public GameObject planet;
    [HideInInspector] public GameObject planetInst;
    public GameObject managers;
    [HideInInspector] public GameObject managersInst;
    public GameObject UI;
    [HideInInspector] public GameObject UIInst;


    public void InstantiateObjects()
    {
        // Debug.Log()
        planetInst = Instantiate(planet);
        playerInst = Instantiate(player);
        UIInst = Instantiate(UI);
        managersInst = Instantiate(managers);

        // Debug.Log(planetInst.)
    }
}
