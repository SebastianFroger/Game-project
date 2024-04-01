using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Shooter
{
    public class HeatBarController : MonoBehaviour
    {
        public UnitStatsSO unitStats;

        private Slider _slider;

        private void OnEnable()
        {
            _slider = GetComponentInChildren<Slider>();
            _slider.maxValue = (float)unitStats.maxHeat;
        }

        public void Update()
        {
            _slider.value = (float)unitStats.heat;
        }
    }
}