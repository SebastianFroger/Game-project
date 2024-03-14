using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class SandStealersController : MonoBehaviour
{
    public TransformRuntimeSet transformRuntimeSet;
    private TMPro.TMP_Text _text;
    private string text = "Harvesters ";

    private void Start()
    {
        _text = GetComponentInChildren<TMPro.TMP_Text>();
        _text.text = text + transformRuntimeSet.Items.Count.ToString();
    }

    public void Update()
    {
        _text.text = text + transformRuntimeSet.Items.Count.ToString();
    }
}

