using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using Unity.VisualScripting;


public class Health : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public bool createInsance;
    public bool invincible;
    public UnityEvent OnStartEvent;
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeathEvent;

    void Start()
    {
        if (createInsance)
        {
            unitStatsSO = Instantiate(unitStatsSO);
        }

        OnStartEvent.Invoke();
    }


    public void TakeDamage(float amount)
    {
        if (invincible) return;

        unitStatsSO.currentHP -= amount;


        if (unitStatsSO.currentHP <= 0)
        {
            if (OnDeathEvent != null)
            {
                OnDeathEvent.Invoke();
            }
        }

        if (OnHitEvent != null && gameObject.activeSelf)
            OnHitEvent.Invoke();
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