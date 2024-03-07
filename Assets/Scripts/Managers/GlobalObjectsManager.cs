using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObjectsManager : Singleton<GlobalObjectsManager>
{
    public GameObject player;
    public GameObject planet;
    public GravityAttractor gravityAttractor;
}
