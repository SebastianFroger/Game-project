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

        private void OnEnable()
        {
            _slider = GetComponentInChildren<Slider>();
            _slider.maxValue = (float)unitStats.currentHP.value;
        }

        public void Update()
        {
            _slider.value = (float)unitStats.currentHP.value;
        }
    }
}