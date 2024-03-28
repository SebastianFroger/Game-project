using System.Reflection;
using System;
using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public UnitStatsSO currentStatsSO;
    public UnitStatsSO baseStatsSO;
    public UnitStatsSO UIStatsSO;
    public UpgradeSO[] allBaseUpgrades;
    private UpgradeSO[] allUpgradesInstance;


    public List<UpgradeSO> GetRandomUpgrades()
    {
        List<UpgradeSO> upgrades = new();

        var random = allUpgradesInstance[UnityEngine.Random.Range(0, allUpgradesInstance.Length)];
        upgrades.Add(random);
        while (upgrades.Count < 4)
        {
            while (upgrades.Contains(random))
            {
                random = allUpgradesInstance[UnityEngine.Random.Range(0, allUpgradesInstance.Length)];
            }
            upgrades.Add(random);
        }

        return upgrades;
    }

    public void SetStartStats()
    {
        ResetStats();
    }

    public void ApplyUpgrade(UnitStatsSO upgradeStats)
    {
        // secondary upgrade
        if (upgradeStats is ISecondary)
        {
            Secondary.Instance.SetSecondary((ISecondary)upgradeStats);
            return;
        }

        // ncrease upgradelevel
        (upgradeStats as UpgradeSO).upgradeLevel++;

        // normal upgrade
        var upgradeFields = upgradeStats.GetAllFieldInfos();
        foreach (var field in upgradeFields)
        {
            var upgradeField = (Upgrade)field.GetValue(upgradeStats);
            var current = (Upgrade)field.GetValue(currentStatsSO);
            if (upgradeField.isPercentage)
            {
                current.value += current.value * (upgradeField.value / 100);
                upgradeField.value += upgradeField.value;
            }
            else if (upgradeField.multiply)
            {
                current.value *= upgradeField.value;
                upgradeField.value *= upgradeField.value;
            }
            else if (upgradeField.isActive)
                current.value = upgradeField.value;
            else
            {
                current.value += upgradeField.value;
                upgradeField.value += upgradeField.value;
            }
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

        // create instance of all upgrades to avoid editor value shit show
        allUpgradesInstance = new UpgradeSO[allBaseUpgrades.Length];
        for (int i = 0; i < allBaseUpgrades.Length; i++)
        {
            allUpgradesInstance[i] = Instantiate(allBaseUpgrades[i]);
        }
    }
}
