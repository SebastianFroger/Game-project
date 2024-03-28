using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeSO : UnitStatsSO
{
    [Header("")]
    [Header("upgrade level")]
    public int upgradeLevel = 1;
    public float priceMultiplier = 2;
    public float effectMultiplier = 2;

    [Header("")]
    [Header("upgrade shop info")]
    public string title;
    public string description;
    public float price;
    public int startPrice;
    public Sprite image;
}