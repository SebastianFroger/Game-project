using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Shooter
{
    public class HealthBarController : MonoBehaviour
    {
        public PlayerStats playerStats;

        private Slider _slider;

        private void Start()
        {
            _slider = GetComponentInChildren<Slider>();
            _slider.value = (float)playerStats.currentHP;
        }

        public void Update()
        {
            _slider.value = ((float)playerStats.currentHP / (float)playerStats.maxHP);
        }
    }
}