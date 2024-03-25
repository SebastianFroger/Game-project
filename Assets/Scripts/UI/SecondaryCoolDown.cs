using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryCoolDown : MonoBehaviour
{
    public Image image;
    public UnitStatsSO unitStats;

    // Update is called once per frame
    void Update()
    {
        if (unitStats.currentCooldownTime.value <= 0)
        {
            image.fillAmount = 0;
            return;
        }

        image.fillAmount = unitStats.currentCooldownTime.value / unitStats.cooldownTime.value;
    }
}
