using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class StatsController : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public TMPro.TMP_Text textTMP;
    public TMPro.TMP_Text textTMPvalues;

    private void Update()
    {
        var playerFields = typeof(UnitStatsSO).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var text = "";
        var values = "";
        foreach (var field in playerFields)
        {
            switch (field.Name)
            {
                case "currentHP":
                    text += $"Health\n";
                    values += $"{field.GetValue(unitStatsSO)}\n";
                    break;

                case "maxHP":
                    text += $"Max Health\n";
                    values += $"{field.GetValue(unitStatsSO)}\n";
                    break;

                case "dammage":
                    text += $"Dammage\n";
                    values += $"{field.GetValue(unitStatsSO)}\n";
                    break;

                case "attackSpeed":
                    text += $"Attack Speed\n";
                    values += $"{field.GetValue(unitStatsSO)}\n";
                    break;

                case "speed":
                    text += $"Movement Speed\n";
                    values += $"{field.GetValue(unitStatsSO)}\n";
                    break;

                case "pickUpRange":
                    text += $"Pick Up Range\n";
                    values += $"{field.GetValue(unitStatsSO)}\n";
                    break;
            }
        }

        textTMP.text = text;
        textTMPvalues.text = values;
    }
}
