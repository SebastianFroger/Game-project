using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shooter
{
    public class Health : MonoBehaviour
    {
        public IntVariable HP;
        public bool reset;
        public IntVariable startHP;
        public IntVariable maxHP;
        public TakeDammageEffect takeDammageEffect;
        public UnityEvent OnHitEvent;
        public UnityEvent OnDeathEvent;

        void Awake()
        {
            if (reset && startHP != null)
            {
                HP.Value = startHP.Value;
                if (maxHP != null)
                    maxHP = startHP;
            }
        }

        public void TakeDamage(int amount)
        {
            HP.Value -= amount;

            takeDammageEffect.Apply(gameObject);

            if (OnHitEvent != null)
                OnHitEvent.Invoke();

            if (HP.Value <= 0)
            {
                if (OnDeathEvent != null)
                    OnDeathEvent.Invoke();
                Destroy(gameObject);
            }
        }
    }
}