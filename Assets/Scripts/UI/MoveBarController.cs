using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Shooter
{
    public class MoveBarController : MonoBehaviour
    {
        public UnitStatsSO unitStats;

        private Slider _slider;

        private void OnEnable()
        {
            _slider = GetComponentInChildren<Slider>();
            _slider.maxValue = (float)unitStats.maxMoveBattery.value;
        }

        public void Update()
        {
            _slider.value = (float)unitStats.currentMoveBattery.value;
        }
    }
}