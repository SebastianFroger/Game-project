using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Shooter;
using UnityEngine;

[CreateAssetMenu]
public class GlobalManagerSO : ScriptableObject
{
    public GameObject player;
    public GameObject planet;
    public GravityAttractor attractor;
}
