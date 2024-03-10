using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitStatsSO : ScriptableObject
{
    public int points;
    public float currentHP;
    public float startHP;
    public float maxHP;
    public float dammage;
    public float startDammage;
    public float attackSpeed;
    public float startAttackSpeed;
    public float speed;
    public float startSpeed;
    public float pickUpRange;
    public float startPickUpRange;

    public void Reset()
    {
        points = 0;
        currentHP = startHP;
        maxHP = startHP;
        speed = startSpeed;
        attackSpeed = startAttackSpeed;
        dammage = startDammage;
        pickUpRange = startPickUpRange;
    }
}