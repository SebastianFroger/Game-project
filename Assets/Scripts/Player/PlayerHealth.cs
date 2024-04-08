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
    public ParticleSystem sparksEffect;

    private float _nextHeatDammageTime = 0;

    private void OnEnable()
    {
        smokeEffect.Stop();
        smokeEffect.Clear();
        smokeEffect.gameObject.SetActive(false);

        sparksEffect.Stop();
        sparksEffect.Clear();
        sparksEffect.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        // heat visual
        if (unitStats.heat > unitStats.maxHeat * 0.8 && smokeEffect.isStopped)
            smokeEffect.Play();

        else if (unitStats.heat < unitStats.maxHeat * 0.8 && smokeEffect.isPlaying)
        {
            smokeEffect.Stop();
            smokeEffect.Clear();
        }

        // HP visual
        if (unitStats.hitPoints < unitStats.maxHitPoints * 0.2)
        {
            sparksEffect.gameObject.SetActive(true);
            sparksEffect.Play();
        }

        else if (unitStats.hitPoints > unitStats.maxHitPoints * 0.2 && sparksEffect.isPlaying)
        {
            sparksEffect.Stop();
            sparksEffect.Clear();
            sparksEffect.gameObject.SetActive(false);
        }

        // shield visual
        if (unitStats.shieldBattery <= 0)
            shieldObject.SetActive(false);
        else
            if (!shieldObject.activeSelf && unitStats.shieldBattery > 10)
            shieldObject.SetActive(true);

        // heat damage player 
        if (unitStats.heat >= unitStats.maxHeat * 0.9f)
            if (Time.fixedTime > _nextHeatDammageTime)
                _nextHeatDammageTime = StatsManager.Instance.TakeHeatDamage();
    }

    public void TakeDamage(float amount)
    {
        if (invincible) return;

        StatsManager.Instance.TakeDamage(amount);

        // death event
        if (unitStats.hitPoints <= 0)
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