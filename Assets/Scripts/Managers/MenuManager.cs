using System.Collections;
using System.Collections.Generic;
using Shooter;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MenuManager : Singleton<MenuManager>
{
    public GameObject mainMenuCanvas;
    public GameObject pauseMenuCanvas;
    public GameObject gameUICanvas;
    public GameObject roundMenuCanvas;
    public GameObject shopMenuCanvas;
    public TMPro.TMP_Text startResumeButtonTMP;

    public GameObject startBtn;
    public GameObject resumeBtn;
    public GameObject readyBtn;
    public GameObject exitShopBtn;

    public void EnableMainMenu(bool enable)
    {
        mainMenuCanvas.SetActive(enable);
        if (enable)
            EventSystem.current.SetSelectedGameObject(startBtn);
    }

    public void EnablePauseMenu(bool enable)
    {
        pauseMenuCanvas.SetActive(enable);
        if (enable)
            EventSystem.current.SetSelectedGameObject(resumeBtn);
    }

    public void EnableRoundMenu(bool enable)
    {
        roundMenuCanvas.SetActive(enable);
        if (enable)
            EventSystem.current.SetSelectedGameObject(readyBtn);
    }

    public void EnableShopMenu(bool enable)
    {
        shopMenuCanvas.SetActive(enable);
        if (enable)
            EventSystem.current.SetSelectedGameObject(exitShopBtn);
    }

    public void EnableGameUI(bool enable)
    {
        gameUICanvas.SetActive(enable);
    }
}
