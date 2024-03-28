using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class MenuCard
{
    public GameObject gameObject;
    public TMPro.TMP_Text title;
    public TMPro.TMP_Text description;
    public TMPro.TMP_Text price;
    public Image image;
}

public class MenuManager : Singleton<MenuManager>
{
    [Header("Menu Canvases")]
    public GameObject mainMenuCanvas;
    public GameObject pauseMenuCanvas;
    public GameObject gameUICanvas;
    public GameObject roundMenuCanvas;
    public GameObject settingsMenuCanvas;
    public GameObject configurationMenuCanvas;

    [Header("Menu Buttons")]
    public GameObject startBtn;
    public GameObject resumeBtn;
    public GameObject rerollBtn;
    public GameObject exitShopBtn;
    public GameObject settingsBackBtn;
    public GameObject configurationStartBtn;

    [Header("Shop Menu Cards")]
    public MenuCard[] shopCards;


    public void EnableMainMenu(bool enable)
    {
        mainMenuCanvas.SetActive(enable);
        if (enable)
            EventSystem.current.SetSelectedGameObject(startBtn);
    }

    public void EnableConfigurationsMenu(bool enable)
    {
        configurationMenuCanvas.SetActive(enable);
        if (enable)
            EventSystem.current.SetSelectedGameObject(configurationStartBtn);
    }

    public void EnablePauseMenu(bool enable)
    {
        pauseMenuCanvas.SetActive(enable);
        if (enable)
            EventSystem.current.SetSelectedGameObject(resumeBtn);
    }

    public void EnableShopMenu(bool enable)
    {
        roundMenuCanvas.SetActive(enable);
        if (enable)
            EventSystem.current.SetSelectedGameObject(rerollBtn);

        rerollBtn.GetComponentInChildren<TMPro.TMP_Text>().text = ShopManager.Instance.GetRerollPrice();
    }

    public void EnableGameUI(bool enable)
    {
        gameUICanvas.SetActive(enable);
    }

    public void EnableSettingsMenu(bool enable)
    {
        settingsMenuCanvas.SetActive(enable);
        if (enable)
            EventSystem.current.SetSelectedGameObject(settingsBackBtn);
    }

    public void SetCardContent(UpgradeSO[] upgrades)
    {
        foreach (var card in shopCards)
        {
            card.gameObject.SetActive(true);
        }

        for (int i = 0; i < shopCards.Length; i++)
        {
            shopCards[i].title.text = $"{upgrades[i].title} Mark {upgrades[i].upgradeLevel}";
            shopCards[i].description.text = upgrades[i].description;
            shopCards[i].price.text = upgrades[i].price.ToString();
            shopCards[i].image.sprite = upgrades[i].image;
        }
    }

    public void DisableUpgradeCard(int index)
    {
        shopCards[index].gameObject.SetActive(false);

        // select new card button
        var next_button = exitShopBtn;
        foreach (var card in shopCards)
        {
            if (card.gameObject.activeSelf)
                next_button = card.gameObject.GetComponentInChildren<UnityEngine.UI.Button>().gameObject;
        }
        EventSystem.current.SetSelectedGameObject(next_button);
    }
}


