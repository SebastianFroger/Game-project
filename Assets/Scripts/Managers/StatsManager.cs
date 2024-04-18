using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class StatsManager : Singleton<StatsManager>
{
    public UnitStatsSO baseStatsSO;
    public UnitStatsSO currentStatsSO;
    public RoundDataSO roundDataSO;

    //******************************************************************************
    // Battery and Heat
    //******************************************************************************

    private void Update()
    {
        // regen and cooldown
        AddMoveBattery(currentStatsSO.moveBatteryRegenPerSecond * Time.deltaTime);
        AddAttackBattery(currentStatsSO.LaserBatteryRegenPerSecond * Time.deltaTime);
        AddShieldBattery(currentStatsSO.shieldBatteryRegenPerSecond * Time.deltaTime);
        RemoveHeat(currentStatsSO.heatCoolingPerSecond * Time.deltaTime);
        UpdateBridgeBarrierCooldown();
    }

    public void AddMoveBattery(float amount)
    {
        currentStatsSO.movementBattery += amount;
        if (currentStatsSO.movementBattery > currentStatsSO.maxMoveBattery)
            currentStatsSO.movementBattery = currentStatsSO.maxMoveBattery;
        if (currentStatsSO.movementBattery < 0)
            currentStatsSO.movementBattery = 0;
    }

    public void AddAttackBattery(float amount)
    {
        currentStatsSO.laserBattery += amount;
        if (currentStatsSO.laserBattery > currentStatsSO.maxLaserBattery)
            currentStatsSO.laserBattery = currentStatsSO.maxLaserBattery;
    }

    public void RemoveAttackBattery(float amount)
    {
        currentStatsSO.laserBattery -= amount;
        if (currentStatsSO.laserBattery < 0)
            currentStatsSO.laserBattery = 0;
    }

    public void AddShieldBattery(float amount)
    {
        currentStatsSO.shieldBattery += amount;
        if (currentStatsSO.shieldBattery > currentStatsSO.maxShieldBattery)
            currentStatsSO.shieldBattery = currentStatsSO.maxShieldBattery;
        if (currentStatsSO.shieldBattery < 0)
            currentStatsSO.shieldBattery = 0;
    }

    public void AddHeat(float amount)
    {
        currentStatsSO.heat += amount;
        if (currentStatsSO.heat > currentStatsSO.maxHeat)
            currentStatsSO.heat = currentStatsSO.maxHeat;
    }

    public void RemoveHeat(float amount)
    {
        currentStatsSO.heat -= amount;
        if (currentStatsSO.heat < 0)
            currentStatsSO.heat = 0;
    }

    public void AddToAllBatteries(float amount)
    {
        AddMoveBattery(amount);
        AddAttackBattery(amount);
        AddShieldBattery(amount);
    }

    //******************************************************************************
    // attack
    //******************************************************************************

    public bool CanAttack()
    {
        // var totalShots = currentStatsSO.lasersPerShot + (currentStatsSO.quadLaserCount * 4);
        return currentStatsSO.laserBattery >= currentStatsSO.laserCost * currentStatsSO.lasersPerShot;
    }

    public float NextAttackTime()
    {
        return Time.time + (1 / currentStatsSO.attacksPerSecond);
    }

    public void OnShot()
    {
        RemoveAttackBattery(currentStatsSO.laserCost);
        AddHeat(currentStatsSO.laserHeatCostPerShot);
    }

    public float CalcDamage()
    {
        float damage = currentStatsSO.damage;
        if (Random.Range(0f, 100f) <= currentStatsSO.critChancePercentage)
            damage *= 1.5f;
        return damage;
    }

    public void EnergySteal()
    {
        AddToAllBatteries(currentStatsSO.energyStealPerLaser / 3);
    }

    //******************************************************************************
    // Move
    //******************************************************************************
    public bool IsMoveBatteryEnough()
    {
        return currentStatsSO.movementBattery >= currentStatsSO.moveBatteryCostPerSecond;
    }

    public void CalcMoveCost(float timeDelta)
    {
        AddMoveBattery(-currentStatsSO.moveBatteryCostPerSecond * timeDelta);
        AddHeat(currentStatsSO.moveHeatCostPerSecond * timeDelta);
    }


    //******************************************************************************
    // Dammage
    //******************************************************************************

    public void TakeDamage(float amount)
    {
        if (currentStatsSO.shieldBattery > 0)
        {
            currentStatsSO.shieldBattery -= amount;
            if (currentStatsSO.shieldBattery < 0)
            {
                currentStatsSO.hitPoints += currentStatsSO.shieldBattery;
                currentStatsSO.shieldBattery = 0;
            }
        }
        else
        {
            currentStatsSO.hitPoints -= amount;
        }
    }

    public float TakeHeatDamage()
    {
        currentStatsSO.hitPoints -= currentStatsSO.heatDammage;
        return Time.time + currentStatsSO.heatDammageInterval;
    }

    //******************************************************************************
    // Bridge Barrier cooldown
    //******************************************************************************

    void UpdateBridgeBarrierCooldown()
    {
        if (currentStatsSO.bridgeCooldownTime > 0)
            currentStatsSO.bridgeCooldownTime -= Time.deltaTime;
        if (currentStatsSO.barrierCooldownTime > 0)
            currentStatsSO.barrierCooldownTime -= Time.deltaTime;
    }


    //******************************************************************************
    // Upgrades calculations
    //******************************************************************************

    public void ApplyUpgrade(UnitStatsSO upgradeStats)
    {
        // // secondary upgrade
        // if (upgradeStats is ISecondary)
        // {
        //     Secondary.Instance.SetSecondary((ISecondary)upgradeStats);
        //     return;
        // }


        // normal upgrade
        var upgradeFields = upgradeStats.GetAllFieldInfos();
        foreach (var field in upgradeFields)
        {
            var upgradeValue = (float)field.GetValue(upgradeStats);
            var currentValue = (float)field.GetValue(currentStatsSO);

            if (upgradeValue == 0)
                continue;

            switch (field.Name)
            {
                // move
                case "maxMoveBattery":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "moveBatteryRegenPerSecond":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "moveBatteryCostPerSecond":
                    currentValue *= upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "moveSpeed":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "moveHeatCostPerSecond":
                    currentValue *= upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;

                // attack
                case "damage":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "attacksPerSecond":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "critChancePercentage":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "maxLaserBattery":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "LaserBatteryRegenPerSecond":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "laserCost":
                    currentValue *= upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "laserHeatCostPerShot":
                    currentValue *= upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;

                // laser
                case "lasersPerShot":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "quadLaserCount":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "piercingCount":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "numberOfTargets":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "energyStealPerLaser":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "enemySlowPercentage":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "enemyKnockBackForce":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;

                // Robots
                case "numberOfAttackRobots":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;

                // shield
                case "maxShieldBattery":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "shieldBatteryRegenPerSecond":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;

                // health
                case "maxHitPoints":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;

                // heat
                case "heat":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "maxHeat":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "heatCoolingPerSecond":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "heatDammage":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "heatDammageInterval":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;

                // secondary
                case "cooldownTime":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "maxCooldownTime":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
                case "cooldownTimeReduction":
                    currentValue += upgradeValue;   // fix
                    field.SetValue(currentStatsSO, currentValue);
                    break;

                // other
                case "pickUpRange":
                    currentValue += upgradeValue;
                    field.SetValue(currentStatsSO, currentValue);
                    break;
            }
        }
    }

    // for UI stats
    public string GetUpgradeValues(UpgradeSO upgrade)
    {
        string values = "";
        foreach (var field in upgrade.GetAllFieldInfos())
        {
            var value = (float)field.GetValue(upgrade);
            if (value == 0)
                continue;

            values += $"{field.Name}: +{value}\n";
        }

        return values;
    }

    public void ResetStats()
    {
        // copy all fields values from baseStatsSO to currentStatsSO
        foreach (var field in currentStatsSO.GetAllFieldInfos())
        {
            var baseVal = field.GetValue(baseStatsSO);
            field.SetValue(currentStatsSO, baseVal);
        }
    }


    // show stats
    public void ShowStats()
    {
        var dps = currentStatsSO.damage * currentStatsSO.attacksPerSecond * currentStatsSO.lasersPerShot;
        var laserCostSec = currentStatsSO.laserCost * currentStatsSO.lasersPerShot * currentStatsSO.attacksPerSecond;
        var laserCost = currentStatsSO.laserCost * currentStatsSO.lasersPerShot;
        var laserHeatCost = currentStatsSO.laserHeatCostPerShot * currentStatsSO.lasersPerShot;
        DebugExt.Log(this, $"DPS {dps} | damage {currentStatsSO.damage} | attacksPerSecond {currentStatsSO.attacksPerSecond} | lasersPerShot {currentStatsSO.lasersPerShot}");
        DebugExt.Log(this, $"laserCostSec {laserCostSec} | laserCost {laserCost} | laserHeatCosttotal {laserHeatCost}");
        DebugExt.Log(this, $"LaserBattery {currentStatsSO.laserBattery} | ShieldBattery {currentStatsSO.shieldBattery} | Heat {currentStatsSO.heat} | MoveBattery {currentStatsSO.movementBattery}");
        DebugExt.Log(this, $"LaserRegen {currentStatsSO.LaserBatteryRegenPerSecond} | heatCooling {currentStatsSO.heatCoolingPerSecond} | shieldRegen {currentStatsSO.shieldBatteryRegenPerSecond} | moveRegen {currentStatsSO.moveBatteryRegenPerSecond}");

    }
}