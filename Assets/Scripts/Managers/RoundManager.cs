using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : Singleton<RoundManager>
{
    public RoundDataSO roundDataSO;
    private float _nextRoundTime;

    public void StartFirstRound()
    {
        roundDataSO.currentRound = 0;
        EnemySpawner.Instance.SetRoundData(roundDataSO);
        _nextRoundTime = Time.fixedTime + roundDataSO.roundDatas[roundDataSO.currentRound].timeSec;
    }

    void Update()
    {
        if (!GameManager.Instance.gameStarted) return;

        roundDataSO.timeCountDown = _nextRoundTime - Time.deltaTime;

        if (Time.fixedTime >= _nextRoundTime)
        {
            EndRound();
        }
    }

    public void EndRound()
    {
        MenuManager.Instance.EnableGameUI(false);
        MenuManager.Instance.EnableRoundMenu(true);
        GameManager.Instance.EnableMenuControls();
        MyObjectPool.Instance.ReleaseAll();
        GameManager.Instance.TimeActive(false);
    }

    public void OnRoundMenuReadyPress()
    {
        roundDataSO.currentRound += 1;
        MenuManager.Instance.EnableRoundMenu(false);
        MenuManager.Instance.EnableGameUI(true);
        GameManager.Instance.EnableGameplayControls();
        GameManager.Instance.TimeActive(true);
        EnemySpawner.Instance.SetRoundData(roundDataSO);
        _nextRoundTime = Time.fixedTime + roundDataSO.roundDatas[roundDataSO.currentRound].timeSec;
    }
}
