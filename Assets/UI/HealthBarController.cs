using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Shooter
{
    public class HealthBarController : MonoBehaviour
    {
        public UnitHealthSO unitHealthSO;

        private VisualElement root;
        private ProgressBar chargeBar;

        private void Start()
        {
            root = GetComponent<UIDocument>().rootVisualElement;

            var actualBar = root.Q(className: "unity-progress-bar__progress");
            actualBar.style.backgroundColor = Color.red;

            chargeBar = root.Q<ProgressBar>("HealthBar");
            chargeBar.value = (float)unitHealthSO.currentHP;
        }

        public void UpdateBar()
        {
            chargeBar.value = ((float)unitHealthSO.currentHP / (float)unitHealthSO.maxHP) * 100f;
        }
    }
}