using System.Reflection;
using System;
using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public UnitStatsSO currentStatsSO;
    public UnitStatsSO baseStatsSO;
    public UpgradeSO[] allUpgrades;
    private List<UpgradeSO> _playerUpgrades = new();

    public UpgradeSO GetRandomUpgrades()
    {
        return allUpgrades[UnityEngine.Random.Range(0, allUpgrades.Length)];
    }

    public void AddUpgrade(UpgradeSO upgradeSO)
    {
        _playerUpgrades.Add(upgradeSO);
    }

    public void SetStartStats()
    {
        ResetStats();
    }

    public void ApplyUpgrade(UnitStatsSO upgradeStats)
    {
        var upgradeFields = upgradeStats.GetAllFieldInfos();
        foreach (var field in upgradeFields)
        {
            var upgrade = (Upgrade)field.GetValue(upgradeStats);
            var current = (Upgrade)field.GetValue(currentStatsSO);
            if (upgrade.isPercentage)
                current.value += (current.value * upgrade.value) / 100f;
            else
                current.value += upgrade.value;
        }
    }

    public void ResetStats()
    {
        // copy all fields values from baseStatsSO to currentStatsSO
        foreach (var field in currentStatsSO.GetAllFieldInfos())
        {
            var current = (Upgrade)field.GetValue(currentStatsSO);
            var basest = (Upgrade)field.GetValue(baseStatsSO);
            current.value = basest.value;
        }
    }
}
