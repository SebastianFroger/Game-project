using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PointsController : MonoBehaviour
{
    public PlayerStatsSO playerStats;

    private TMPro.TMP_Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<TMPro.TMP_Text>();
        _text.text = "Points " + playerStats.points.ToString();
    }

    public void Update()
    {
        _text.text = "Points " + playerStats.points.ToString();
    }
}

