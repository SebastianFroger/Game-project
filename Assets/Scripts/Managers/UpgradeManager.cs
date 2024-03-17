// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public UnitStatsSO unitStatsSO;
    public UpgradeSO[] upgradeSO;

    public UpgradeSO GetRandomUpgrades()
    {
        return upgradeSO[Random.Range(0, upgradeSO.Length)];
    }

    public void ApplyUpgrade(UpgradeSO upgradeSO)
    {
        upgradeSO.ApplyUpgrade(unitStatsSO);
    }
}
