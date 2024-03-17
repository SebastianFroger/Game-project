using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBlinking : MonoBehaviour
{
    public Color color;
    public float blinkTimeSec;
    public int blinkCount;

    private TMPro.TMP_Text _text;
    private bool _isBlinking;

    private void Awake()
    {
        _text = GetComponentInChildren<TMPro.TMP_Text>();
        _text.gameObject.SetActive(false);
        _text.color = color;
    }

    public void StartBlink()
    {
        if (_isBlinking) return;
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        _isBlinking = true;
        for (int i = 0; i < blinkCount; i++)
        {
            _text.gameObject.SetActive(true);
            yield return new WaitForSeconds(blinkTimeSec);
            _text.gameObject.SetActive(false);
            yield return new WaitForSeconds(blinkTimeSec);
        }
        _isBlinking = false;
    }
}
