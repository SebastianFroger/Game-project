using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : Singleton<RoundManager>
{
    public RoundDataSO roundDataSO;
    public UnitStatsSO playerStatsSO;
    private float _nextRoundTime = 0f;
    private UnitStatsSO _savedPlayerStatsSO;
    private bool _stopTime;

    private void Start()
    {
        roundDataSO.currentRound = 0;
        _stopTime = false;
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.gameStarted) return;
        if (_stopTime) return;

        if (_nextRoundTime == 0)
        {
            _nextRoundTime = Time.fixedTime + roundDataSO.roundDatas[roundDataSO.currentRound].timeSec;
        }

        roundDataSO.timeCountDown = _nextRoundTime - Time.fixedTime;

        if (_nextRoundTime <= Time.fixedTime)
        {
            EndRound();
        }
    }

    public void StopRoundTime()
    {
        _stopTime = true;
    }

    public void StartFirstRound()
    {
        MenuManager.Instance.EnableMainMenu(false);
        MenuManager.Instance.EnableGameUI(true);
        GameManager.Instance.EnableGameplayControls();
        GameManager.Instance.TimeActive(true);
        _nextRoundTime = Time.fixedTime + roundDataSO.roundDatas[roundDataSO.currentRound].timeSec;
        SavePlayerStats();
    }

    public void EndRound()
    {
        MenuManager.Instance.EnableGameUI(false);
        MenuManager.Instance.EnableShopMenu(true);
        ShopManager.Instance.SetShopContent();
        GameManager.Instance.EnableMenuControls();
        MyObjectPool.Instance.ReleaseAll();
        GameManager.Instance.TimeActive(false);
    }

    public void OnRestartRoundPress()
    {
        MyObjectPool.Instance.ReleaseAll();
        MenuManager.Instance.EnableMainMenu(false);
        MenuManager.Instance.EnableGameUI(true);
        MenuManager.Instance.EnablePauseMenu(false);
        GameManager.Instance.EnableGameplayControls();
        GameManager.Instance.TimeActive(true);
        _nextRoundTime = Time.fixedTime + roundDataSO.roundDatas[roundDataSO.currentRound].timeSec;

        ResetRoundStats(true);
    }

    public void OnRoundMenuReadyPress()
    {
        roundDataSO.currentRound += 1;
        MenuManager.Instance.EnableShopMenu(false);
        MenuManager.Instance.EnableGameUI(true);
        GameManager.Instance.EnableGameplayControls();
        GameManager.Instance.TimeActive(true);
        _nextRoundTime = Time.fixedTime + roundDataSO.roundDatas[roundDataSO.currentRound].timeSec;

        ResetRoundStats();

        _stopTime = false;
        SavePlayerStats();
    }

    void SavePlayerStats()
    {
        if (_savedPlayerStatsSO != null)
            Destroy(_savedPlayerStatsSO);
        _savedPlayerStatsSO = Instantiate(playerStatsSO);
    }

    void ResetRoundStats(bool setPoints = false)
    {
        playerStatsSO.currentHP.value = playerStatsSO.maxHP.value;
        playerStatsSO.currentAttackBattery.value = playerStatsSO.maxAttackBattery.value;
        playerStatsSO.currentShieldBattery.value = playerStatsSO.maxShieldBattery.value;
        playerStatsSO.currentMoveBattery.value = playerStatsSO.maxMoveBattery.value;
        playerStatsSO.currentHeat.value = 0;

        if (setPoints)
            playerStatsSO.points.value = _savedPlayerStatsSO.points.value;
    }
}
