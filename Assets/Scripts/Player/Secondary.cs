using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Secondary : Singleton<Secondary>
{
    public UnitStatsSO unitStats;
    public UpgradeSO secondary;

    private float _nextActionTime = 0f;
    private bool _isPressed = false;

    private void Update()
    {
        if (unitStats.currentCooldownTime.value > 0)
        {
            unitStats.currentCooldownTime.value -= Time.deltaTime;
            _isPressed = false;
        }
    }

    void OnSecondary(InputAction.CallbackContext ctx)
    {
        DebugExt.Log(this, $"OnSecondary");
        if (Time.time < _nextActionTime || _isPressed) return;

        (secondary as ISecondary).Execute(transform);
        unitStats.currentCooldownTime.value = unitStats.cooldownTime.value;

        _nextActionTime = Time.time + unitStats.cooldownTime.value;

        _isPressed = true;
    }
}
