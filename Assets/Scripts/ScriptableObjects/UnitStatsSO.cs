using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;



[CreateAssetMenu]
public class UnitStatsSO : ScriptableObject
{
    public float points;
    public float currentHP;
    public float maxHP;
    public float dammage;
    public float attackSpeed;
    public float speed;
    public float pickUpRange;
}