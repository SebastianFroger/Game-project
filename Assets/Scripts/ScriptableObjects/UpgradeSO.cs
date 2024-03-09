using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

[CreateAssetMenu]
public class UpgradeSO : PlayerStatsSO
{
    [Header("upgrade shop info")]
    public string title;
    public string description;
    public int price;

    public PlayerStatsSO playerCurrentStats;

    public void ApplyUpgrade(PlayerStatsSO playerCurrentStats = null)
    {
        var upgradeFields = typeof(PlayerStatsSO).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var playerFields = typeof(PlayerStatsSO).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var upgradeField in upgradeFields)
        {
            var playerField = Array.Find(playerFields, f => f.Name == upgradeField.Name);
            if (playerField == null) return;

            var upgradeValue = upgradeField.GetValue(this);
            var playerValue = playerField.GetValue(playerCurrentStats);

            if (playerValue.GetType() == typeof(int) && (int)upgradeField.GetValue(this) != 0)
            {
                playerField.SetValue(playerCurrentStats, (int)playerValue + (int)upgradeValue);
            }

            if (playerValue.GetType() == typeof(float) && (float)upgradeField.GetValue(this) != 0)
            {
                playerField.SetValue(playerCurrentStats, (float)playerValue + (float)upgradeValue);
            }
        }
    }
}