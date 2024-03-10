using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Shooter
{
    public class HealthBarController : MonoBehaviour
    {
        public UnitStatsSO unitStats;

        private Slider _slider;

        private void Start()
        {
            _slider = GetComponentInChildren<Slider>();
            _slider.value = (float)unitStats.currentHP;
        }

        public void Update()
        {
            if (unitStats.currentHP > unitStats.maxHP)
                unitStats.currentHP = unitStats.maxHP;

            _slider.value = ((float)unitStats.currentHP / (float)unitStats.maxHP);
        }
    }
}