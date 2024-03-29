using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;


public class RobotHealth : MonoBehaviour, IHealth
{
    public UnitStatsSO unitStatsSO;
    public UnitStatsSO playerStatsSO;
    public bool createInsance;
    public UnityEvent OnStartEvent;
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeathEvent;

    private UnitStatsSO unitStatsSOInstance;
    public delegate void MyDelegate(Transform enemy);
    public static MyDelegate OnDeathEventDelegate;

    [SerializeField] private bool _isInvincible;

    void Start()
    {

        OnStartEvent.Invoke();
    }

    private void OnEnable()
    {
        unitStatsSOInstance = Instantiate(unitStatsSO);
        unitStatsSOInstance.currentHP.value = unitStatsSO.maxHP.value * playerStatsSO.attackRobotCount.value;
        unitStatsSOInstance.damage.value = unitStatsSO.damage.value * playerStatsSO.attackRobotCount.value;
    }

    public void TakeDamage(float amount, bool ignoreShield = false)
    {
        if (_isInvincible)
            return;

        unitStatsSOInstance.currentHP.value -= amount;


        if (unitStatsSOInstance.currentHP.value <= 0)
        {
            OnDeathEventDelegate?.Invoke(transform);
            OnDeathEvent?.Invoke();
        }

        if (OnHitEvent != null && gameObject.activeSelf)
            OnHitEvent?.Invoke();
    }

    private void OnDestroy()
    {
        if (createInsance)
        {
#if UNITY_EDITOR
            DestroyImmediate(unitStatsSOInstance);
#else
                Destroy(unitStatsSOInstance);
#endif
        }
    }
}