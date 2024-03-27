using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeSO : UnitStatsSO
{
    [Header("upgrade shop info")]
    public string title;
    public string description;
    public int price;
    public int startPrice;
    public Sprite image;
}