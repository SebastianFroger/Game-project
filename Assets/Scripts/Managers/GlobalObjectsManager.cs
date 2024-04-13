using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class GlobalObjectsManager : Singleton<GlobalObjectsManager>
{
    public GameObject player;
    public NavMeshSurface navMeshSurface;
}
