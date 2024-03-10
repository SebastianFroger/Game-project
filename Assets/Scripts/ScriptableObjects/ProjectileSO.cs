using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileSO : ScriptableObject
{
    public GameObject prefab;
    public int dammage;
    public float speed = 50f;
    public AudioClip shootAudio;
    public AudioClip hitAudio;
    public ParticleSystem hitEffect;
}
