using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    public UnitStatsSO unitStats;
    public RoundDataSO roundDataSO;
    public int startPrice;
    public int priceIncreasePrRound;

    private UpgradeSO[] _shopUpgrades;

    public void OnShopOpen()
    {
        _shopUpgrades = UpgradeManager.Instance.GetRandomUpgrades();
        MenuManager.Instance.SetCardContent(_shopUpgrades);
    }

    public void OnBuyItem(int index)
    {
        var upgrade = _shopUpgrades[index];
        if (upgrade.price <= unitStats.points)
        {
            MenuManager.Instance.DisableUpgradeCard(index);
            UpgradeManager.Instance.ApplyUpgrade(upgrade);
            unitStats.points -= upgrade.price;
        }
    }

    private void SetItemPrice()
    {
        foreach (var item in _shopUpgrades)
        {
            item.price = startPrice + (priceIncreasePrRound * roundDataSO.currentRound);
        }
    }
}
