using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    public UnitStatsSO unitStats;
    public RoundDataSO roundDataSO;
    public int startPrice;
    public int priceIncreasePrRound;

    private int _roundPrice;
    private List<UpgradeSO> _currentUpgrades = new();

    public void SetShopContent()
    {
        _currentUpgrades.Clear();

        foreach (var card in MenuManager.Instance.shopCards)
        {
            var upgrade = UpgradeManager.Instance.GetRandomUpgrades();
            upgrade.price = _roundPrice;
            _currentUpgrades.Add(upgrade);
            card.price.text = upgrade.price.ToString();
        }
        MenuManager.Instance.SetCardContent(_currentUpgrades.ToArray());
    }

    public string GetRerollPrice()
    {
        _roundPrice = startPrice + (priceIncreasePrRound * roundDataSO.currentRound);
        return $"Reroll ({_roundPrice})";
    }

    public void OnReroll()
    {
        if (unitStats.points.value < _roundPrice)
            return;
        unitStats.points.value -= _roundPrice;
        SetShopContent();
    }

    public void OnBuyItem(int index)
    {
        var upgrade = _currentUpgrades[index];
        if (upgrade.price <= unitStats.points.value)
        {
            MenuManager.Instance.DisableUpgradeCard(index);
            UpgradeManager.Instance.ApplyUpgrade(upgrade);
            unitStats.points.value -= upgrade.price;
        }
    }
}
