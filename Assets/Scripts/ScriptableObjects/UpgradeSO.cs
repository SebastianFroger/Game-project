using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeSO : UnitStatsSO
{
    [Header("")]
    [Header("upgrade level")]
    public int upgradeLevel = 1;

    [Header("")]
    [Header("upgrade shop info")]
    public string title;
    public string description;
    public float price;
    public int startPrice;
    public Sprite image;
}