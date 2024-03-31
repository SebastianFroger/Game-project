using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BatteryManager : Singleton<BatteryManager>
{
    public UnitStatsSO unitStatsSO;

    private void Update()
    {
        // regen and cooldown
        AddMoveBattery(unitStatsSO.moveBatteryRegenRate.value * Time.deltaTime);
        AddAttackBattery(unitStatsSO.attackBatteryRegenRate.value * Time.deltaTime);
        AddShieldBattery(unitStatsSO.shieldBatteryRegenRate.value * Time.deltaTime);
        RemoveHeat(unitStatsSO.heatCoolingRate.value * Time.deltaTime);
    }

    public void AddMoveBattery(float amount)
    {
        unitStatsSO.currentMoveBattery.value += amount;
        if (unitStatsSO.currentMoveBattery.value > unitStatsSO.maxMoveBattery.value)
            unitStatsSO.currentMoveBattery.value = unitStatsSO.maxMoveBattery.value;
    }

    public void AddAttackBattery(float amount)
    {
        unitStatsSO.currentAttackBattery.value += amount;
        if (unitStatsSO.currentAttackBattery.value > unitStatsSO.maxAttackBattery.value)
            unitStatsSO.currentAttackBattery.value = unitStatsSO.maxAttackBattery.value;
    }

    public void AddShieldBattery(float amount)
    {
        unitStatsSO.currentShieldBattery.value += amount;
        if (unitStatsSO.currentShieldBattery.value > unitStatsSO.maxShieldBattery.value)
            unitStatsSO.currentShieldBattery.value = unitStatsSO.maxShieldBattery.value;
    }

    public void AddHeat(float amount)
    {
        unitStatsSO.currentHeat.value += amount;
        if (unitStatsSO.currentHeat.value > unitStatsSO.maxHeat.value)
            unitStatsSO.currentHeat.value = unitStatsSO.maxHeat.value;
    }

    public void RemoveHeat(float amount)
    {
        unitStatsSO.currentHeat.value -= amount;
        if (unitStatsSO.currentHeat.value < 0)
            unitStatsSO.currentHeat.value = 0;
    }

    public void AddToAllBatteries(float amount)
    {
        AddMoveBattery(amount);
        AddAttackBattery(amount);
        AddShieldBattery(amount);
    }
}