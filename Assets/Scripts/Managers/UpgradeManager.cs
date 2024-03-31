using System.Reflection;
using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public UnitStatsSO currentStatsSO;
    public UnitStatsSO baseStatsSO;
    public RoundDataSO roundDataSO;
    public UpgradeSO[] allBaseUpgrades;
    public bool allRandomUpgrades;
    public bool loadAllUpgradesFromDisk;

    private List<UpgradeSO> allUpgradesInstance = new();


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
            UpgradeSO random = allUpgradesInstance[UnityEngine.Random.Range(0, allUpgradesInstance.Count - 1)];
            while (upgrades.Contains(random))
            {
                random = allUpgradesInstance[UnityEngine.Random.Range(0, allUpgradesInstance.Count - 1)];
            }

            upgrades.Add(random);
        }

        return upgrades;
    }

    public void CheckForUpgradeTier()
    {
        if (roundDataSO.currentRound == 0)
            AddUpgradeTier(1);

        if (roundDataSO.currentRound == 1)  // starts from 0 array index
            AddUpgradeTier(2);

        if (roundDataSO.currentRound == 7)
            AddUpgradeTier(3);

        if (roundDataSO.currentRound == 11)
            AddUpgradeTier(4);
    }

    void AddUpgradeTier(int tierLvl)
    {
        foreach (var upgrade in allBaseUpgrades)
        {
            var upgradeInst = Instantiate(upgrade);

            // set upgrade level
            upgradeInst.upgradeLevel = tierLvl;

            // set price
            if (tierLvl > 1)
                upgradeInst.startPrice = (int)(upgradeInst.startPrice * tierLvl * .8f);


            allUpgradesInstance.Add(upgradeInst);

            // apply upgrade
            foreach (var field in upgradeInst.GetAllFieldInfos())
            {
                var upgradeField = (Upgrade)field.GetValue(upgradeInst);
                if (upgradeField.value == 0)
                    continue;
                upgradeField.value *= tierLvl;
            }
        }
    }

    public string GetUpgradeValues(UpgradeSO upgrade)
    {
        string values = "";
        foreach (var field in upgrade.GetAllFieldInfos())
        {
            var upgradeField = (Upgrade)field.GetValue(upgrade);
            if (upgradeField.value == 0)
                continue;

            if (upgradeField.isPercentage)
            {
                values += $"{field.Name}: {upgradeField.value}%\n";
                continue;
            }
            if (upgradeField.multiply)
            {
                values += $"{field.Name}: x{upgradeField.value}\n";
                continue;
            }

            values += $"{field.Name}: +{upgradeField.value}\n";
        }

        return values;
    }
    public void ApplyUpgrade(UnitStatsSO upgradeStats)
    {
        // secondary upgrade
        if (upgradeStats is ISecondary)
        {
            Secondary.Instance.SetSecondary((ISecondary)upgradeStats);
            return;
        }

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
    }

#if UNITY_EDITOR
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
#endif
}

