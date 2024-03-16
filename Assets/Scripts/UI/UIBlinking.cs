using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBlinking : MonoBehaviour
{
    public Color color;
    public float blinkTimeSec;
    public int blinkCount;

    private TMPro.TMP_Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<TMPro.TMP_Text>();
        _text.gameObject.SetActive(false);
        _text.color = color;
    }

    public void StartBlink()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            _text.gameObject.SetActive(true);
            yield return new WaitForSeconds(blinkTimeSec);
            _text.gameObject.SetActive(false);
            yield return new WaitForSeconds(blinkTimeSec);
        }
    }
}
