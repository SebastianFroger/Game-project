using System.Collections;
using System.Collections.Generic;
using Shooter;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : Singleton<MenuManager>
{
    public GameObject mainMenuCanvas;
    public GameObject gameUICanvas;
    public TMPro.TMP_Text startResumeButtonTMP;

    public void MenuOpen(bool open)
    {
        mainMenuCanvas.SetActive(open);
    }

    public void ShowGameUI(bool show)
    {
        gameUICanvas.SetActive(show);
    }

    public void OnQuitPress()
    {
        Application.Quit();
    }

    public void OnStartPress()
    {
        startResumeButtonTMP.GetComponent<TMPro.TMP_Text>().text = "Resume";
        mainMenuCanvas.SetActive(false);
        gameUICanvas.SetActive(true);
    }
}
