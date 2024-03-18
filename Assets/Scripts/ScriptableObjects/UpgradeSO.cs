using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using UnityEngine.UI;

[CreateAssetMenu]
public class UpgradeSO : UnitStatsSO
{
    [Header("upgrade shop info")]
    public string title;
    public string description;
    public int price;
    public Sprite image;
}