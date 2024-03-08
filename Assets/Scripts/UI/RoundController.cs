using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoundController : Singleton<RoundController>
{
    public RoundDataSO roundDataSO;
    public TMPro.TMP_Text roundCountText;
    public TMPro.TMP_Text roundCountDownText;

    private string roundText = "Round ";
    private string timeText = "Time ";

    private void Start()
    {
        roundCountText.text = roundDataSO.currentRound.ToString();
        roundCountDownText.text = timeText + 0.ToString();
    }

    public void Update()
    {
        roundCountText.text = roundText + roundDataSO.currentRound.ToString();
        roundCountDownText.text = timeText + Mathf.Round(roundDataSO.timeCountDown - Time.fixedTime).ToString();
    }
}

