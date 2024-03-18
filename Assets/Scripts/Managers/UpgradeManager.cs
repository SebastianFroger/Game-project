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
        ApplyUpgrade(baseStatsSO);
    }

    public void ApplyUpgrade(UnitStatsSO upgradeStats)
    {
        var upgradeStatsFields = typeof(UnitStatsSO).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var field in upgradeStatsFields)
        {
            var upgradeValue = field.GetValue(upgradeStats);
            var playerValue = field.GetValue(currentStatsSO);
            field.SetValue(currentStatsSO, (float)playerValue + (float)upgradeValue);
        }
    }

    public void ResetStats()
    {
        var upgradeFields = typeof(UnitStatsSO).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var field in upgradeFields)
        {
            field.SetValue(currentStatsSO, 0f);
        }
    }
}
