using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Shooter
{
    public class HealthBarController : MonoBehaviour
    {
        public IntVariable playerHP;
        public IntVariable playerMaxHP;

        private VisualElement root;
        private ProgressBar chargeBar;

        private void Start()
        {
            root = GetComponent<UIDocument>().rootVisualElement;

            var actualBar = root.Q(className: "unity-progress-bar__progress");
            actualBar.style.backgroundColor = Color.red;

            chargeBar = root.Q<ProgressBar>("HealthBar");
            chargeBar.value = (float)playerHP.Value;
        }

        public void UpdateBar()
        {
            chargeBar.value = ((float)playerHP.Value / (float)playerMaxHP.Value) * 100f;
        }
    }
}