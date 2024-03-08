using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : Singleton<RoundManager>
{
    public int nrOfRounds = 10;
    public float roundTime = 30f;

    public RoundDataSO roundDataSO;
    private float _nextRoundTime;

    public void StartFirstRound()
    {
        roundDataSO.currentRound = 1;
        _nextRoundTime = Time.time + roundTime;
        roundDataSO.timeCountDown = _nextRoundTime - Time.fixedTime;
    }

    void Update()
    {
        if (!GameManager.Instance.gameStarted) return;

        roundDataSO.timeCountDown = _nextRoundTime - Time.deltaTime;

        if (Time.fixedTime >= _nextRoundTime)
        {
            RoundEnd();
        }
    }

    void RoundEnd()
    {
        DebugExt.Log(this, "RoundEnded");
        GameManager.Instance.EndRound();
        roundDataSO.currentRound += 1;
        _nextRoundTime = Time.time + roundTime;
        roundDataSO.timeCountDown = _nextRoundTime - Time.time;
    }
}
