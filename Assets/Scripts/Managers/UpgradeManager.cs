using System.Reflection;
using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public UnitStatsSO currentStatsSO;
    public UnitStatsSO baseStatsSO;
    public UnitStatsSO UIStatsSO;
    public UpgradeSO[] allBaseUpgrades;
    public bool allRandomUpgrades;
    public bool loadAllUpgradesFromDisk;

    private UpgradeSO[] allUpgradesInstance;


    public List<UpgradeSO> GetRandomUpgrades()
    {
        List<UpgradeSO> upgrades = new();

        if (!allRandomUpgrades)
        {
            for (int i = 0; i < 4; i++)
            {
                upgrades.Add(allUpgradesInstance[i]);
            }
            return upgrades;
        }

        for (int i = 0; i < 4; i++)
        {
            UpgradeSO random = allUpgradesInstance[UnityEngine.Random.Range(0, allUpgradesInstance.Length)];
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

        // increase upgradelevel
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
                if (!upgradeField.dontStackValue)
                    upgradeField.value += upgradeField.value;
            }
            else if (upgradeField.multiply)
            {
                current.value *= upgradeField.value;
                if (!upgradeField.dontStackValue)
                    upgradeField.value *= upgradeField.value;
            }
            else if (upgradeField.isActive)
                current.value = upgradeField.value;
            else
            {
                current.value += upgradeField.value;
                if (!upgradeField.dontStackValue)
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

    private void OnValidate()
    {
        if (loadAllUpgradesFromDisk)
        {
            Array.Clear(allBaseUpgrades, 0, 0);

            var assets = AssetDatabase.FindAssets("t: scriptableobject", new string[] { "Assets/GameData/Upgrades" });

            List<UpgradeSO> loadedAssets = new();
            foreach (var guid in assets)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var loaded = AssetDatabase.LoadAssetAtPath(path, typeof(UpgradeSO)) as UpgradeSO;
                loadedAssets.Add(loaded);
            }
            allBaseUpgrades = loadedAssets.ToArray();

            loadAllUpgradesFromDisk = false;
        }
    }
}

