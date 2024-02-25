using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : Health
{
    public PlayerData playerData;
    public static UnityAction<int, int> playerHitEvent;
    public static UnityAction playerDeadEvent;

    void Awake()
    {
        base.Start();
        playerData.currentHP = playerData.startHP;
    }

    public override void TakeDamage(int amount)
    {
        playerData.currentHP -= amount;
        StartCoroutine("DamageFlash");
        playerHitEvent?.Invoke(playerData.currentHP, playerData.startHP);

        if (playerData.currentHP <= 0)
            playerDeadEvent?.Invoke();
    }
}
