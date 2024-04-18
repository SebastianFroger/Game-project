using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;


public class Health : MonoBehaviour, IHealth
{
    public UnitStatsSO unitStatsSO;
    public bool createInsance;
    public UnityEvent OnStartEvent;
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeathEvent;

    public delegate void MyDelegate(Transform enemy);
    public static MyDelegate OnDeathEventDelegate;

    [SerializeField] private bool _isInvincible;

    void Start()
    {
        if (createInsance)
        {
            unitStatsSO = Instantiate(unitStatsSO);
        }

        OnStartEvent.Invoke();
    }

    private void OnEnable()
    {
        unitStatsSO.hitPoints = unitStatsSO.maxHitPoints + Mathf.Round((RoundManager.Instance.roundDataSO.currentRound + 1) / 2);
    }

    public void TakeDamage(float amount)
    {
        if (_isInvincible)
            return;

        unitStatsSO.hitPoints -= amount;

        if (unitStatsSO.hitPoints <= 0)
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
            DestroyImmediate(unitStatsSO);
#else
                Destroy(unitStatsSO);
#endif
        }
    }
}




#if UNITY_EDITOR
[CustomEditor(typeof(Health))]
public class HealthEditor : Editor
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