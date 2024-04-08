using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class CrystalsController : MonoBehaviour
{
    public UnitStatsSO unitStats;

    private TMPro.TMP_Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<TMPro.TMP_Text>();
        _text.text = "Crystals " + unitStats.crystals.ToString();
    }

    public void Update()
    {
        _text.text = "Crystals " + unitStats.crystals.ToString();
    }
}

