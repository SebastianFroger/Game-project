using System.Collections;
using System.Collections.Generic;
using Shooter;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : Singleton<MenuManager>
{
    public GameObject _mainMenuCanvas;
    public TMPro.TMP_Text _startResumeButtonTMP;

    public void MenuOpen()
    {
        _mainMenuCanvas.SetActive(true);
    }

    public void MenuClose()
    {
        _mainMenuCanvas.SetActive(false);
    }

    public void OnQuitPress()
    {
        Application.Quit();
    }

    public void OnStartPress()
    {
        _mainMenuCanvas.SetActive(false);
        _startResumeButtonTMP.GetComponent<TMPro.TMP_Text>().text = "Resume";
    }
}
