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
        RobotsManager.Instance.InstantiateRobots();
        GameManager.Instance.TimeActive(true);

        _nextRoundTime = Time.fixedTime + roundDataSO.roundDatas[roundDataSO.currentRound].timeSec;

        ResetBatteriesAndHeat();
        SavePlayerCurrentStats();
    }

    public void EndRound()
    {
        MenuManager.Instance.EnableGameUI(false);
        UpgradeManager.Instance.CheckForUpgradeTier();
        MenuManager.Instance.EnableShopMenu(true);
        ShopManager.Instance.SetShopContent();
        GameManager.Instance.EnableMenuControls();
        MyObjectPool.Instance.ReleaseAll();
        RobotsManager.Instance.ResetRobots();
        GameManager.Instance.TimeActive(false);
    }

    public void OnRestartRoundPress()
    {
        MyObjectPool.Instance.ReleaseAll();
        MenuManager.Instance.EnableMainMenu(false);
        MenuManager.Instance.EnableGameUI(true);
        MenuManager.Instance.EnablePauseMenu(false);
        GameManager.Instance.EnableGameplayControls();
        RobotsManager.Instance.InstantiateRobots();
        GameManager.Instance.TimeActive(true);
        _nextRoundTime = Time.fixedTime + roundDataSO.roundDatas[roundDataSO.currentRound].timeSec;

        ResetBatteriesAndHeat();
        playerStatsSO.points = _savedPlayerStatsSO.points;
    }

    public void OnRoundMenuReadyPress()
    {
        roundDataSO.currentRound += 1;
        MenuManager.Instance.EnableShopMenu(false);
        MenuManager.Instance.EnableGameUI(true);
        GameManager.Instance.EnableGameplayControls();
        RobotsManager.Instance.InstantiateRobots();
        GameManager.Instance.TimeActive(true);
        _nextRoundTime = Time.fixedTime + roundDataSO.roundDatas[roundDataSO.currentRound].timeSec;

        ResetBatteriesAndHeat();

        _stopTime = false;
        SavePlayerCurrentStats();
    }

    void SavePlayerCurrentStats()
    {
        if (_savedPlayerStatsSO != null)
            Destroy(_savedPlayerStatsSO);
        _savedPlayerStatsSO = Instantiate(playerStatsSO);
    }

    void ResetBatteriesAndHeat()
    {
        playerStatsSO.hitPoints = playerStatsSO.maxHitPoints;
        playerStatsSO.laserBattery = playerStatsSO.maxLaserBattery;
        playerStatsSO.shieldBattery = playerStatsSO.maxShieldBattery;
        playerStatsSO.movementBattery = playerStatsSO.maxMoveBattery;
        playerStatsSO.heat = 0;
    }
}
