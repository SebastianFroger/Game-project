using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Shooter
{
    public class AttackBarController : MonoBehaviour
    {
        public UnitStatsSO unitStats;

        private Slider _slider;

        private void Start()
        {
            _slider = GetComponentInChildren<Slider>();
            _slider.maxValue = (float)unitStats.maxAttackBattery.value;
        }

        public void Update()
        {
            _slider.value = (float)unitStats.currentAttackBattery.value;
        }
    }
}