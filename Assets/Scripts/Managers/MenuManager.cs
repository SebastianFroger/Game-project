using System.Collections;
using System.Collections.Generic;
using Shooter;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : Singleton<MenuManager>
{
    public GameObject mainMenuCanvas;
    public GameObject pauseMenuCanvas;
    public GameObject gameUICanvas;
    public GameObject roundMenuCanvas;
    public TMPro.TMP_Text startResumeButtonTMP;

    public void EnableMainMenu(bool enable)
    {
        mainMenuCanvas.SetActive(enable);
    }

    public void EnablePauseMenu(bool enable)
    {
        pauseMenuCanvas.SetActive(enable);
    }

    public void EnableRoundMenu(bool enable)
    {
        roundMenuCanvas.SetActive(enable);
    }

    public void EnableGameUI(bool enable)
    {
        gameUICanvas.SetActive(enable);
    }
}
