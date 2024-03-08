using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using Unity.VisualScripting;


[ExecuteAlways]
public class Health : MonoBehaviour
{
    public UnitStatsSO unitStatsSO;
    public bool createInsance;
    public UnityEvent OnStartEvent;
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeathEvent;

    void Start()
    {
        if (createInsance)
        {
            unitStatsSO = Instantiate(unitStatsSO);
        }

        // this is for the lpayer HP bar to update
        unitStatsSO.currentHP = unitStatsSO.startHP;
        OnStartEvent.Invoke();
    }

    // reset enemy HP when taken from pool
    void OnEnable()
    {
        unitStatsSO.currentHP = unitStatsSO.startHP;
    }

    public void TakeDamage(int amount)
    {
        unitStatsSO.currentHP -= amount;


        if (unitStatsSO.currentHP <= 0)
        {
            if (OnDeathEvent != null)
            {
                Debug.Log("death event");
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