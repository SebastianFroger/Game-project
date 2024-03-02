using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using Unity.VisualScripting;

namespace Shooter
{
    [ExecuteAlways]
    public class Health : MonoBehaviour
    {
        public UnitHealthSO healthSO;
        public bool createInsance;
        public UnityEvent OnStartEvent;
        public UnityEvent OnHitEvent;
        public UnityEvent OnDeathEvent;

        void Start()
        {
            if (createInsance)
            {
                healthSO = Instantiate(healthSO);
            }

            // this is for the lpayer HP bar to update
            healthSO.currentHP = healthSO.startHP;
            OnStartEvent.Invoke();
        }

        // reset enemy HP when taken from pool
        void OnEnable()
        {
            healthSO.currentHP = healthSO.startHP;
        }

        public void TakeDamage(int amount)
        {
            healthSO.currentHP -= amount;


            if (healthSO.currentHP <= 0)
            {
                if (OnDeathEvent != null)
                    OnDeathEvent.Invoke();
            }

            if (OnHitEvent != null && gameObject.activeSelf)
                OnHitEvent.Invoke();
        }

        private void OnDestroy()
        {
            if (createInsance)
            {
#if UNITY_EDITOR
                DestroyImmediate(healthSO);
#else
                Destroy(healthSO);
#endif
            }
        }
    }




# if UNITY_EDITOR
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
}