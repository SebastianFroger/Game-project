using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Shooter;

[Serializable]
public class Upgrade
{
    public bool isPercentage;
    public float value;
}

[CreateAssetMenu]
public class UnitStatsSO : ScriptableObject
{
    [Header("Points")]
    public Upgrade points;
    public Upgrade configurationPoints;

    [Header("Movement")]
    public Upgrade currentMoveBattery;
    public Upgrade maxMoveBattery;
    public Upgrade moveBatteryRegenRate;
    public Upgrade moveCostPerSecond;
    public Upgrade moveSpeed;
    public Upgrade moveHeatCostPerSecond;

    [Header("Attack")]
    public Upgrade currentAttackBattery;
    public Upgrade maxAttackBattery;
    public Upgrade attackBatteryRegenRate;
    public Upgrade attackCost;
    public Upgrade attackHeatCostPerShot;

    [Header("Shield")]
    public Upgrade currentShieldBattery;
    public Upgrade maxShieldBattery;
    public Upgrade shieldBatteryRegenRate;

    [Header("Health")]
    public Upgrade currentHP;
    public Upgrade maxHP;

    [Header("Attack")]
    public Upgrade dammage;
    public Upgrade attackSpeed;

    [Header("Heat")]
    public Upgrade currentHeat;
    public Upgrade maxHeat;
    public Upgrade heatCoolingRate;
    public Upgrade heatDammage;
    public Upgrade heatDammageRate;

    [Header("Other")]
    public Upgrade pickUpRange;
    public Upgrade critChance;

    // public Upgrade doublePointsChance;
    // public Upgrade lifeSteal;
    // public Upgrade luck;

    // get all fields of the class
    public FieldInfo[] GetAllFieldInfos()
    {
        return typeof(UnitStatsSO).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    }
}