using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    private VisualElement root;

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        var actualBar = root.Q(className: "unity-progress-bar__progress");
        actualBar.style.backgroundColor = Color.red;

        PlayerHealth.playerHitEvent += UpdateBar;
    }

    private void UpdateBar(int currentVal, int maxVal)
    {
        ProgressBar chargeBar = root.Q<ProgressBar>("HealthBar");
        chargeBar.value = ((float)currentVal / (float)maxVal) * 100;
    }
}