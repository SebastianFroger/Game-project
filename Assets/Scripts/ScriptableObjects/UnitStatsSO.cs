using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Shooter;

[Serializable]
public class Upgrade
{
    public bool isActive;
    public bool isPercentage;
    public bool multiply;
    public float value;
}

[CreateAssetMenu]
public class UnitStatsSO : ScriptableObject
{
    [Header("Points")]
    public Upgrade points;
    public Upgrade configurationPoints;

    [Header("")]
    [Header("Movement")]
    public Upgrade currentMoveBattery;
    public Upgrade maxMoveBattery;
    public Upgrade moveBatteryRegenRate;
    public Upgrade moveCostPerSecond;
    public Upgrade moveSpeed;
    public Upgrade moveHeatCostPerSecond;

    [Header("")]
    [Header("Attack")]
    public Upgrade damage;
    public Upgrade attackSpeed;
    public Upgrade critChance;
    public Upgrade currentAttackBattery;
    public Upgrade maxAttackBattery;
    public Upgrade attackBatteryRegenRate;
    public Upgrade attackCost;
    public Upgrade attackHeatCostPerShot;

    [Header("")]
    [Header("Laser")]
    public Upgrade laserCount;
    public Upgrade laserAroundCount;
    public Upgrade piercingCount;
    public Upgrade targetsCount;
    public Upgrade bouncingShotCount;
    public Upgrade energySteal;
    public Upgrade slowEnemies;
    public Upgrade knockBackEnemies;

    [Header("")]
    [Header("Robots")]
    public Upgrade attackRobotCount;


    [Header("")]
    [Header("Shield")]
    public Upgrade currentShieldBattery;
    public Upgrade maxShieldBattery;
    public Upgrade shieldBatteryRegenRate;

    [Header("")]
    [Header("Health")]
    public Upgrade currentHP;
    public Upgrade maxHP;

    [Header("")]
    [Header("Heat")]
    public Upgrade currentHeat;
    public Upgrade maxHeat;
    public Upgrade heatCoolingRate;
    public Upgrade heatDammage;
    public Upgrade heatDammageRate;

    [Header("")]
    [Header("Secondary")]
    public Upgrade currentCooldownTime;
    public Upgrade cooldownTime;

    [Header("")]
    [Header("Other")]
    public Upgrade pickUpRange;

    // public Upgrade doublePointsChance;
    // public Upgrade lifeSteal;
    // public Upgrade luck;

    // get all fields of the class
    public FieldInfo[] GetAllFieldInfos()
    {
        return typeof(UnitStatsSO).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    }
}