using System.Reflection;
using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class UpgradeManager : Singleton<UpgradeManager>
{
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
                var value = (float)field.GetValue(upgradeInst);
                if (value == 0)
                    continue;
                value *= tierLvl;
            }
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

