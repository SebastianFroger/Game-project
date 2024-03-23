using System.Reflection;
using System;
using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public UnitStatsSO currentStatsSO;
    public UnitStatsSO baseStatsSO;
    public UpgradeSO[] allUpgrades;
    private List<UnitStatsSO> _playerUpgrades = new();

    public UpgradeSO GetRandomUpgrades()
    {
        var random = UnityEngine.Random.Range(0, allUpgrades.Length);
        return allUpgrades[random];
    }

    // use this to display accumulated upgrade stats in the UI, instead of full stats
    public void AddUpgrade(UpgradeSO upgradeSO)
    {
        _playerUpgrades.Add((UnitStatsSO)upgradeSO);
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
                current.value += current.value * (upgrade.value / 100);
            else
                current.value += upgrade.value;
        }

        AddUpgrade((UpgradeSO)upgradeStats);
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
