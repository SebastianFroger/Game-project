using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : Singleton<RoundManager>
{
    public RoundDataSO roundDataSO;
    private float _nextRoundTime = 0f;

    public void StartFirstRound()
    {
        roundDataSO.currentRound = 0;
        _nextRoundTime = Time.fixedTime + roundDataSO.roundDatas[roundDataSO.currentRound].timeSec;
    }

    void Update()
    {
        if (!GameManager.Instance.gameStarted) return;

        roundDataSO.timeCountDown = _nextRoundTime - Time.deltaTime;

        if (_nextRoundTime <= Time.fixedTime)
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
        Planet.Instance.ResetScale();
    }

    public void OnRoundMenuReadyPress()
    {
        roundDataSO.currentRound += 1;
        MenuManager.Instance.EnableRoundMenu(false);
        MenuManager.Instance.EnableGameUI(true);
        GameManager.Instance.EnableGameplayControls();
        GameManager.Instance.TimeActive(true);
        _nextRoundTime = Time.fixedTime + roundDataSO.roundDatas[roundDataSO.currentRound].timeSec;
    }
}
