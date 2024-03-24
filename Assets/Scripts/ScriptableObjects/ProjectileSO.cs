using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileSO : ScriptableObject
{
    public GameObject prefab;
    public int damage;
    public float speed = 50f;
    public AudioClip shootAudio;
    public AudioClip hitAudio;
    public ParticleSystem hitEffect;
}
