using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatsController : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public TMPro.TMP_Text textTMP;
    public TMPro.TMP_Text textTMPvalues;

    private void Update()
    {
        var text = "";
        var values = "";
        bool colorChange = false;
        foreach (var field in unitStatsSO.GetAllFieldInfos())
        {
            var name = field.Name;
            string value = (field.GetValue(unitStatsSO) as Upgrade).value.ToString();

            if (name.StartsWith("current")) continue;
            if (name == "maxHP") continue;

            colorChange = !colorChange;
            if (colorChange)
            {
                name = "<color=white>" + name + "</color>";
                value = "<color=white>" + value + "</color>";
            }
            else
            {
                name = "<color=#D5D5D5>" + name + "</color>";
                value = "<color=#D5D5D5>" + value + "</color>";
            }

            text += name + "\n";
            values += value + "\n";
        }

        textTMP.text = text;
        textTMPvalues.text = values;
    }
}
