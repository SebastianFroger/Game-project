using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq.Expressions;

public class StatsController : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public TMPro.TMP_Text textTMP;
    public TMPro.TMP_Text textTMPvalues;

    private void Update()
    {
        var text = "";
        var values = "";
        foreach (var field in unitStatsSO.GetAllFieldInfos())
        {
            var name = field.Name;
            var upgrade = (Upgrade)field.GetValue(unitStatsSO);
            switch (field.Name)
            {
                case "maxHP":
                    text += $"Health\n";
                    values += $"{upgrade.value}\n";
                    break;

                case "dammage":
                    text += $"Dammage\n";
                    values += $"{upgrade.value}\n";
                    break;

                case "attackSpeed":
                    text += $"Attack Speed\n";
                    values += $"{upgrade.value}\n";
                    break;

                case "speed":
                    text += $"Movement Speed\n";
                    values += $"{upgrade.value}\n";
                    break;

                case "pickUpRange":
                    text += $"Pick Up Range\n";
                    values += $"{upgrade.value}\n";
                    break;
            }
        }

        textTMP.text = text;
        textTMPvalues.text = values;
    }
}
