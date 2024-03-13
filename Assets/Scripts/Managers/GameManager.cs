using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;



public class GameManager : Singleton<GameManager>
{
    public UnitStatsSO unitStatsSO;
    private bool _isPaused;
    public bool gameStarted;
    public PlayerInput _playerInput;
    private string _actionMapPlayerControls = "Player Control";
    private string _actionMenuControls = "Menu Control";

    void Start()
    {
        gameStarted = false;
        TimeActive(false);
        MenuManager.Instance.EnableMainMenu(true);
        MenuManager.Instance.EnableGameUI(false);
        MenuManager.Instance.EnableRoundMenu(false);
        MenuManager.Instance.EnablePauseMenu(false);
        MenuManager.Instance.EnableShopMenu(false);
        EnableMenuControls();
    }


    // buttons and menus
    public void OnStarButtonPress() // start button in menu
    {
        ResetStats();
        MenuManager.Instance.EnableMainMenu(false);
        MenuManager.Instance.EnableGameUI(true);
        EnableGameplayControls();
        TimeActive(true);
        gameStarted = true;
        RoundManager.Instance.StartFirstRound();
    }

    public void OnResumeButtonPress() // resume button in menu
    {
        PauseMenuToggle();
    }

    public void PauseMenuToggle()
    {
        if (!gameStarted) return;

        _isPaused = !_isPaused;

        if (_isPaused)
        {
            TimeActive(false);
            MenuManager.Instance.EnableGameUI(false);
            MenuManager.Instance.EnablePauseMenu(true);
            EnableMenuControls();
        }
        else
        {
            MenuManager.Instance.EnablePauseMenu(false);
            MenuManager.Instance.EnableGameUI(true);
            EnableGameplayControls();
            TimeActive(true);
        }
    }

    public void ShopMenuOpen()
    {
        MenuManager.Instance.EnableGameUI(false);
        MenuManager.Instance.EnableShopMenu(true);
        ShopManager.Instance.OnShopOpen();
        EnableMenuControls();
        TimeActive(false);
    }

    public void ShopMenuClose()
    {
        MenuManager.Instance.EnableShopMenu(false);
        MenuManager.Instance.EnableGameUI(true);
        EnableGameplayControls();
        TimeActive(true);
    }

    public void OnRestartButtonPress()
    {
        GameReset();
    }

    public void OnQuitAppButtonPress()
    {
        Application.Quit();
    }

    public void TimeActive(bool active)
    {
        Time.timeScale = active == true ? 1f : 0f;
    }

    private void ResetStats()
    {
        unitStatsSO.Reset();
    }

    public void GameReset()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    // actioncontrols
    public void EnableGameplayControls()
    {
        _playerInput.SwitchCurrentActionMap(_actionMapPlayerControls);
    }

    public void EnableMenuControls()
    {
        _playerInput.SwitchCurrentActionMap(_actionMenuControls);
    }
}
