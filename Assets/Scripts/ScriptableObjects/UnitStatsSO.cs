using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;


[CreateAssetMenu]
public class UnitStatsSO : ScriptableObject
{
    [Header("Points")]
    public float points;
    public float configurationPoints;

    [Header("")]
    [Header("Movement")]
    public float movementBattery;
    public float maxMoveBattery;
    public float moveBatteryRegenPerSecond;
    public float moveBatteryCostPerSecond;
    public float moveSpeed;
    public float moveHeatCostPerSecond;

    [Header("")]
    [Header("Attack")]
    public float damage;
    public float attacksPerSecond;
    public float critChancePercentage;
    public float laserBattery;
    public float maxLaserBattery;
    public float LaserBatteryRegenPerSecond;
    public float laserCost;
    public float laserHeatCostPerShot;

    [Header("")]
    [Header("Laser")]
    public float lasersPerShot;
    public float quadLaserCount;
    public float piercingCount;
    public float numberOfTargets;
    public float energyStealPerLaser;
    public float enemySlowPercentage;
    public float enemyKnockBackForce;

    [Header("")]
    [Header("Robots")]
    public float numberOfAttackRobots;


    [Header("")]
    [Header("Shield")]
    public float shieldBattery;
    public float maxShieldBattery;
    public float shieldBatteryRegenPerSecond;

    [Header("")]
    [Header("Health")]
    public float hitPoints;
    public float maxHitPoints;

    [Header("")]
    [Header("Heat")]
    public float heat;
    public float maxHeat;
    public float heatCoolingPerSecond;
    public float heatDammage;
    public float heatDammageInterval;

    [Header("")]
    [Header("Secondary")]
    public float cooldownTime;
    public float maxCooldownTime;
    public float cooldownTimeReduction;

    [Header("")]
    [Header("Other")]
    public float pickUpRange;

    // public float doublePointsChance;
    // public float luck;

    // get all fields of the class
    public FieldInfo[] GetAllFieldInfos()
    {
        return typeof(UnitStatsSO).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    }
}