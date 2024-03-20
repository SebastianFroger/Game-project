using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;


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

        StartCoroutine(RegenHealth());
    }


    public void TakeDamage(float amount)
    {
        if (invincible) return;

        if (UnityEngine.Random.Range(0, 100) < unitStatsSO.dodgeChance.value)
        {
            return;
        }

        amount -= unitStatsSO.armor.value;
        unitStatsSO.currentHP.value -= amount;

        if (unitStatsSO.currentHP.value <= 0)
        {
            if (OnDeathEvent != null)
            {
                OnDeathEvent.Invoke();
                StopCoroutine(RegenHealth());
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

    IEnumerator RegenHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            unitStatsSO.currentHP.value += unitStatsSO.HPRegen.value;
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