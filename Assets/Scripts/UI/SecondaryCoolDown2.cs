using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryCoolDown2 : MonoBehaviour
{
    public Image image;
    public UnitStatsSO unitStats;

    // Update is called once per frame
    void Update()
    {
        if (unitStats.barrierCooldownTime <= 0)
        {
            image.fillAmount = 0;
            return;
        }

        image.fillAmount = unitStats.barrierCooldownTime / unitStats.barrierCooldownTimeMax;
    }
}
