using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    public UnitStatsSO unitStats;
    public RoundDataSO roundDataSO;
    public int startPrice;
    public int priceIncreasePrRound;
    public int rerollPrice;

    private List<UpgradeSO> _currentUpgrades = new();

    public void OnShopOpen()
    {
        _currentUpgrades.Clear();

        foreach (var card in MenuManager.Instance.shopCards)
        {
            var upgrade = UpgradeManager.Instance.GetRandomUpgrades();
            _currentUpgrades.Add(upgrade);
            card.price.text = (startPrice + (priceIncreasePrRound * roundDataSO.currentRound)).ToString();
        }
        MenuManager.Instance.SetCardContent(_currentUpgrades.ToArray());
    }

    public void OnReroll()
    {
        if (unitStats.points < rerollPrice)
            return;
        unitStats.points -= rerollPrice;
        OnShopOpen();
    }

    public void OnBuyItem(int index)
    {
        var upgrade = _currentUpgrades[index];
        if (upgrade.price <= unitStats.points)
        {
            MenuManager.Instance.DisableUpgradeCard(index);
            UpgradeManager.Instance.ApplyUpgrade(upgrade);
            unitStats.points -= upgrade.price;
        }
    }
}
