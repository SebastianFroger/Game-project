using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Secondary : Singleton<Secondary>
{
    public UnitStatsSO unitStats;
    public UpgradeSO secondary;
    public Image image;
    public Image imageDark;

    private float _nextActionTime = 0f;

    private void Update()
    {
        if (unitStats.cooldownTime > 0)
        {
            unitStats.cooldownTime -= Time.deltaTime;
        }
    }

    public void SetSecondary(ISecondary secondary)
    {
        var inst = Instantiate(secondary as UpgradeSO);
        if (this.secondary == null)
        {
            this.secondary = inst;
            image.sprite = inst.image;
            return;
        }

        if (this.secondary.GetType() == secondary.GetType())
        {
            (secondary as ISecondary).Upgrade();
        }
        else
        {
            image.sprite = inst.image;
            this.secondary = inst;
        }
    }

    // void OnSecondary()
    // {
    //     if (Time.time < _nextActionTime || secondary == null) return;

    //     (secondary as ISecondary).Execute(transform);
    //     unitStats.cooldownTime = unitStats.cooldownTime;

    //     _nextActionTime = Time.time + unitStats.cooldownTime;
    // }
}
