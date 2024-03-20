using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

[Serializable]
public class Upgrade
{
    public bool isPercentage;
    public float value;
}

[CreateAssetMenu]
public class UnitStatsSO : ScriptableObject
{
    public Upgrade points;
    public Upgrade currentHP;
    public Upgrade maxHP;
    public Upgrade dammage;
    public Upgrade attackSpeed;
    public Upgrade speed;
    public Upgrade pickUpRange;
    public Upgrade HPRegen;
    public Upgrade critChance;
    public Upgrade armor;
    public Upgrade dodgeChance;
    public Upgrade doublePointsChance;
    public Upgrade lifeSteal;
    public Upgrade luck;

    // get all fields of the class
    public FieldInfo[] GetAllFieldInfos()
    {
        return typeof(UnitStatsSO).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    }
}