using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;


// IHealth interface
public interface IHealth
{
    void TakeDamage(float amount, bool ignoreShield = false);
}

public class PlayerHealth : MonoBehaviour, IHealth
{
    public UnitStatsSO unitStats;
    public GameObject shieldObject;
    public bool invincible;
    public UnityEvent OnStartEvent;
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeathEvent;

    private float _nextHeatDammageTime = 0;

    private void FixedUpdate()
    {
        // heat cooldown
        if (unitStats.heatCoolingRate.value > 0)
        {
            unitStats.currentHeat.value -= unitStats.heatCoolingRate.value * Time.fixedDeltaTime;
            if (unitStats.currentHeat.value < 0)
                unitStats.currentHeat.value = 0;
        }

        // heat damage player 
        if (unitStats.currentHeat.value >= unitStats.maxHeat.value)
        {
            unitStats.currentHeat.value = unitStats.maxHeat.value;

            if (Time.fixedTime > _nextHeatDammageTime)
            {
                TakeDamage(unitStats.heatDammage.value, true);
                _nextHeatDammageTime = Time.fixedTime + unitStats.heatDammageRate.value;
            }
        }

        // shield regen
        if (unitStats.shieldBatteryRegenRate.value > 0)
        {
            unitStats.currentShieldBattery.value += unitStats.shieldBatteryRegenRate.value * Time.fixedDeltaTime;
            if (unitStats.currentShieldBattery.value > unitStats.maxShieldBattery.value)
                unitStats.currentShieldBattery.value = unitStats.maxShieldBattery.value;
        }

        if (unitStats.currentShieldBattery.value <= 0)
        {
            shieldObject.SetActive(false);
        }
        else
        {
            if (!shieldObject.activeSelf && unitStats.currentShieldBattery.value > 10)
                shieldObject.SetActive(true);
        }
    }

    public void TakeDamage(float amount, bool ignoreShield = false)
    {
        if (invincible) return;

        if (!ignoreShield && shieldObject.activeSelf)
        {
            unitStats.currentShieldBattery.value -= amount;
            return;
        }

        unitStats.currentHP.value -= amount;

        if (unitStats.currentHP.value <= 0)
        {
            if (OnDeathEvent != null)
            {
                OnDeathEvent.Invoke();
            }
        }

        if (OnHitEvent != null && gameObject.activeSelf)
            OnHitEvent.Invoke();
    }

    // IEnumerator RegenHealth()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(1);
    //         unitStats.currentHP.value += unitStats.HPRegen.value;
    //     }
    // }
}




#if UNITY_EDITOR
[CustomEditor(typeof(Health))]
public class PlayerHealthEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Health myTarget = (Health)target;
        base.DrawDefaultInspector();

        if (GUILayout.Button("OnHitEvent"))
        {
            myTarget.OnHitEvent.Invoke();
        }
        if (GUILayout.Button("OnDeathEvent"))
        {
            myTarget.OnDeathEvent.Invoke();
        }

        if (GUILayout.Button("Kill Unit"))
        {
            myTarget.TakeDamage(999999999);
        }
    }
}
#endif