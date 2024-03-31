using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


// 4 tiers in x base stat 


public class ShopManager : Singleton<ShopManager>
{
    public UnitStatsSO unitStats;
    public RoundDataSO roundDataSO;
    public TMPro.TMP_Text rerollText;

    private int _rerollPrice;
    private int _nrOfRerolls;
    private List<UpgradeSO> _currentUpgrades = new();


    //******************************************************************************
    // shop items and calculations
    //******************************************************************************

    public void SetShopContent()
    {
        // get new Upgrades
        _currentUpgrades.Clear();
        _currentUpgrades = UpgradeManager.Instance.GetRandomUpgrades();

        for (int i = 0; i < MenuManager.Instance.shopCards.Length; i++)
        {
            // set price
            _currentUpgrades[i].price = CalcItemPrice(_currentUpgrades[i]);
            MenuManager.Instance.shopCards[i].price.text = _currentUpgrades[i].price.ToString();
        }

        MenuManager.Instance.SetCardContent(_currentUpgrades.ToArray());

        CalculateRerollPrice();
        rerollText.text = _rerollPrice.ToString();
    }


    float CalcItemPrice(UpgradeSO upgrade)
    {
        // Final Price =  (Base_Price + Wave + (Base_Price * 0.1 * Wave)) * Shop_Price
        return Mathf.Round(upgrade.startPrice + roundDataSO.currentRound + upgrade.startPrice * 0.1f * roundDataSO.currentRound);
    }

    //******************************************************************************
    // Reroll
    //******************************************************************************

    public void OnReroll()
    {
        // take points
        if (unitStats.points < _rerollPrice)
            return;
        unitStats.points -= _rerollPrice;

        // increase reroll counter
        _nrOfRerolls++;

        SetShopContent();
    }

    void CalculateRerollPrice()
    {
        // roundnr +  Rounddown(0.5 * Wave Number) * Reroll Number
        var currRound = (float)roundDataSO.currentRound + 1;
        _rerollPrice = Mathf.RoundToInt((currRound + (0.5f * currRound) * _nrOfRerolls));

        if (_rerollPrice == 0)
            _rerollPrice = 1;
    }

    public void OnBuyItem(int index)
    {
        var upgrade = _currentUpgrades[index];
        if (upgrade.price <= unitStats.points)
        {
            MenuManager.Instance.DisableUpgradeCard(index);
            StatsManager.Instance.ApplyUpgrade(upgrade);
            unitStats.points -= upgrade.price;
        }
    }
}
