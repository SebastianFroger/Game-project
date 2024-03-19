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
            _slider.value = (float)unitStats.currentHP.value;
        }

        public void Update()
        {
            if (unitStats.currentHP.value > unitStats.maxHP.value)
                unitStats.currentHP.value = unitStats.maxHP.value;

            _slider.value = ((float)unitStats.currentHP.value / (float)unitStats.maxHP.value);
        }
    }
}