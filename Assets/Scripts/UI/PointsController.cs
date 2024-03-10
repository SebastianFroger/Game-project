using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PointsController : MonoBehaviour
{
    public UnitStatsSO unitStats;

    private TMPro.TMP_Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<TMPro.TMP_Text>();
        _text.text = "Points " + unitStats.points.ToString();
    }

    public void Update()
    {
        _text.text = "Points " + unitStats.points.ToString();
    }
}

