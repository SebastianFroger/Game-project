using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class MenuCard
{
    public TMPro.TMP_Text title;
    public TMPro.TMP_Text description;
}

public class MenuManager : Singleton<MenuManager>
{
    [Header("Menu Canvases")]
    public GameObject mainMenuCanvas;
    public GameObject pauseMenuCanvas;
    public GameObject gameUICanvas;
    public GameObject roundMenuCanvas;
    public GameObject shopMenuCanvas;
    public TMPro.TMP_Text startResumeButtonTMP;

    [Header("Menu Buttons")]
    public GameObject startBtn;
    public GameObject resumeBtn;
    public GameObject readyBtn;
    public GameObject exitShopBtn;

    [Header("Shop Menu Cards")]
    public MenuCard[] menuCards;

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

    public void SetCardContent(UpgradeSO[] upgrades)
    {
        for (int i = 0; i < menuCards.Length; i++)
        {
            menuCards[i].title.text = upgrades[i].title;
            menuCards[i].description.text = upgrades[i].description;
        }
    }
}


