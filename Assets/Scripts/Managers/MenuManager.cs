using System.Collections;
using System.Collections.Generic;
using Shooter;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : Singleton<MenuManager>
{
    public GameObject _mainMenuCanvas;

    public bool isPaused { get; private set; }

    public void MenuOpen()
    {
        _mainMenuCanvas.SetActive(true);

        Debug.Log("MenuOpen");
    }

    public void MenuClose()
    {
        _mainMenuCanvas.SetActive(false);

        Debug.Log("MenuClose");
    }

    public void OnQuitPress()
    {
        Application.Quit();
    }

    public void OnStartPress()
    {
        _mainMenuCanvas.SetActive(false);
        // _startResumeTMP.GetComponent<TMPro.TMP_Text>().text = "Resume";

        Debug.Log("OnStartResumePress");
    }
}
