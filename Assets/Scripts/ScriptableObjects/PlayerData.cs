using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public int startHP;
    public int currentHP;
    public int moveSpeed;
    public float attacksPerSec;
    public AudioClip moveSound;
}
