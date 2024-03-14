using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : Singleton<RoundManager>
{
    public RoundDataSO roundDataSO;
    private float _nextRoundTime = 0f;

    private void Start()
    {
        roundDataSO.currentRound = 0;
    }

    void Update()
    {
        if (!GameManager.Instance.gameStarted) return;

        if (_nextRoundTime == 0)
        {
            _nextRoundTime = Time.fixedTime + roundDataSO.roundDatas[roundDataSO.currentRound].timeSec;
        }


        roundDataSO.timeCountDown = _nextRoundTime - Time.deltaTime;

        if (_nextRoundTime <= Time.fixedTime)
        {
            EndRound();
        }
    }

    public void StartFirstRound()
    {
        MenuManager.Instance.EnableMainMenu(false);
        MenuManager.Instance.EnableRoundMenu(false);
        MenuManager.Instance.EnableGameUI(true);
        GameManager.Instance.EnableGameplayControls();
        GameManager.Instance.TimeActive(true);
        PlanetDiggerManager.Instance.SetupRound();
        _nextRoundTime = Time.fixedTime + roundDataSO.roundDatas[roundDataSO.currentRound].timeSec;
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
        PlanetDiggerManager.Instance.SetupRound();
        _nextRoundTime = Time.fixedTime + roundDataSO.roundDatas[roundDataSO.currentRound].timeSec;
    }
}
