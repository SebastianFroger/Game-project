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

    private void Start()
    {
        roundDataSO.currentRound = 1;
        _nextRoundTime = Time.time + roundTime;
        roundDataSO.timeCountDown = _nextRoundTime - Time.fixedTime;
    }

    void Update()
    {
        roundDataSO.timeCountDown = _nextRoundTime - Time.deltaTime;

        if (Time.fixedTime >= _nextRoundTime)
        {
            RoundEndState();
        }
    }

    void RoundEndState()
    {
        GameManager.Instance.TogglePauseState();
        roundDataSO.currentRound += 1;
        _nextRoundTime = Time.time + roundTime;
        roundDataSO.timeCountDown = _nextRoundTime - Time.time;
    }
}
