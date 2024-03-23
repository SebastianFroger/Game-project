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
        foreach (var field in unitStatsSO.GetAllFieldInfos())
        {
            var name = field.Name;
            var upgrade = (Upgrade)field.GetValue(unitStatsSO);

            if (name.StartsWith("current")) continue;
            if (name == "maxHP") continue;

            text += name + "\n";
            values += upgrade.value + "\n";
        }

        textTMP.text = text;
        textTMPvalues.text = values;
    }
}
