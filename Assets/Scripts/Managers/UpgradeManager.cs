// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public PlayerStatsSO playerStatsSO;
    public UpgradeSO[] upgradeSO;

    public UpgradeSO[] GetRandomUpgrades()
    {
        return new UpgradeSO[] {
            upgradeSO[Random.Range(0, upgradeSO.Length)],
            upgradeSO[Random.Range(0, upgradeSO.Length)],
            upgradeSO[Random.Range(0, upgradeSO.Length)]
        };
    }

    public void ApplyUpgrade(UpgradeSO upgradeSO)
    {
        DebugExt.Log(this, "Apply upgrade " + upgradeSO.name);
    }
}
