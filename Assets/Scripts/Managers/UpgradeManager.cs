// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [Header("Upgrades List")]
    public UpgradeSO[] upgradeSO;

    public UpgradeSO[] GetRandomUpgrades()
    {
        return new UpgradeSO[] {
            upgradeSO[Random.Range(0, upgradeSO.Length)],
            upgradeSO[Random.Range(0, upgradeSO.Length)],
            upgradeSO[Random.Range(0, upgradeSO.Length)]
        };
    }
}
