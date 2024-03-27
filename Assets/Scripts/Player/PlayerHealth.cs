using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerHealth : MonoBehaviour, IHealth
{
    public UnitStatsSO unitStats;
    public GameObject shieldObject;
    public bool invincible;
    public UnityEvent OnStartEvent;
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeathEvent;
    public ParticleSystem smokeEffect;
    public GameObject sparksEffect;

    private float _nextHeatDammageTime = 0;

    private void OnEnable()
    {
        // smokeEffect.SetActive(false);
        smokeEffect.Stop();
        smokeEffect.Clear();
        sparksEffect.SetActive(false);
    }

    private void FixedUpdate()
    {
        // heat visual
        if (unitStats.currentHeat.value > unitStats.maxHeat.value * 0.8 && smokeEffect.isStopped)
        {
            smokeEffect.Play();
        }
        else if (unitStats.currentHeat.value < unitStats.maxHeat.value * 0.8 && smokeEffect.isPlaying)
        {
            smokeEffect.Stop();
        }

        // HP visual
        if (unitStats.currentHP.value < unitStats.maxHP.value * 0.2)
        {
            sparksEffect.SetActive(true);
        }
        else if (unitStats.currentHP.value > unitStats.maxHP.value * 0.2 && smokeEffect.isPlaying)
        {
            sparksEffect.SetActive(false);
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
            BatteryManager.Instance.AddShieldBattery(-amount);
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
}