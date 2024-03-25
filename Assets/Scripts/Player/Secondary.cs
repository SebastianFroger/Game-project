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
        if (unitStats.currentCooldownTime.value > 0)
        {
            unitStats.currentCooldownTime.value -= Time.deltaTime;
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

    void OnSecondary()
    {
        if (Time.time < _nextActionTime) return;

        (secondary as ISecondary).Execute(transform);
        unitStats.currentCooldownTime.value = unitStats.cooldownTime.value;

        _nextActionTime = Time.time + unitStats.cooldownTime.value;
    }
}
