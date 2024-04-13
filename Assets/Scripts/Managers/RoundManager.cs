using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : Singleton<RoundManager>
{
    public RoundDataSO roundDataSO;
    public UnitStatsSO playerStatsSO;
    public GameObject crystalPrefab;
    public LayerMask groundLayer;
    float mapSizeX = 100f;
    float mapSizeZ = 100f;

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

        SpawnCrystals();
        ResetBatteriesAndHeat();
        SavePlayerCurrentStats();

        StatsManager.Instance.ShowStats();
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

        SpawnCrystals();
        ResetBatteriesAndHeat();

        _stopTime = false;
        SavePlayerCurrentStats();

        StatsManager.Instance.ShowStats();
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
        playerStatsSO.points = 0;
        playerStatsSO.crystals = 0;
    }

    void SpawnCrystals()
    {
        var amount = (roundDataSO.currentRound + 1) * 2;
        for (int i = 0; i < amount; i++)
        {
            var crystal = Instantiate(crystalPrefab);
            crystal.transform.position = SpawnPoint();
        }
    }

    private Vector3 SpawnPoint()
    {
        Vector3 raycastHit = Vector3.zero;
        var randomPoint = GetRandomPoint();

        while (raycastHit == Vector3.zero)
        {
            randomPoint = GetRandomPoint();
            if (Physics.Raycast(randomPoint, Vector3.down, out RaycastHit hit2, 1000f, groundLayer))
            {
                raycastHit = hit2.point;
            }
        }

        return randomPoint;
    }

    Vector3 GetRandomPoint()
    {
        return new Vector3(Random.Range(-mapSizeX, mapSizeX), 0, Random.Range(-mapSizeZ, mapSizeZ));
    }
}
