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
            Upgrade upgrade = (field.GetValue(unitStatsSO) as Upgrade);
            string value = (Mathf.Round(upgrade.value * 10f) * 0.1f).ToString();

            if (name == "maxHP") continue;

            // set color
            colorChange = !colorChange;
            if (colorChange)
            {
                name = "<color=white>" + name + "</color>";
                value = "<color=white>" + value + "</color>";
            }
            else
            {
                name = "<color=#CCCCCC>" + name + "</color>";
                value = "<color=#CCCCCC>" + value + "</color>";
            }

            text += name + "\n";
            values += value + "\n";
        }

        textTMP.text = text;
        textTMPvalues.text = values;
    }
}
