// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public UnitStatsSO unitStatsSO;
    public UpgradeSO[] upgradeSO;
    public int upgradesCount = 3;
    private List<UpgradeSO> _currentUpgrades = new();

    public UpgradeSO[] GetRandomUpgrades()
    {
        _currentUpgrades.Clear();

        while (_currentUpgrades.Count < upgradesCount)
        {
            var newUpgrade = upgradeSO[Random.Range(0, upgradeSO.Length - 1)];
            while (_currentUpgrades.Contains(newUpgrade))
            {
                newUpgrade = upgradeSO[Random.Range(0, upgradeSO.Length - 1)];
            }
            _currentUpgrades.Add(newUpgrade);
        }

        return _currentUpgrades.ToArray();
    }

    public void ApplyUpgrade(UpgradeSO upgradeSO)
    {
        upgradeSO.ApplyUpgrade(unitStatsSO);
    }
}
