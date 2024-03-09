using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;



public class GameManager : Singleton<GameManager>
{
    public PlayerStats playerStats;
    private bool _isPaused;
    public bool gameStarted;
    public PlayerInput _playerInput;
    private string _actionMapPlayerControls = "Player Controls";
    private string _actionMenuControls = "Menu Controls";

    void Start()
    {
        gameStarted = false;
        TimeActive(false);
        MenuManager.Instance.EnableMainMenu(true);
        MenuManager.Instance.EnableGameUI(false);
        MenuManager.Instance.EnableRoundMenu(false);
        MenuManager.Instance.EnablePauseMenu(false);
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

    public void EndRound()
    {
        TimeActive(false);
        MenuManager.Instance.EnableRoundMenu(true);
        MenuManager.Instance.EnableGameUI(false);
        EnableMenuControls();
        MyObjectPool.Instance.Clear();
    }

    public void OnRoundMenuReadyPress()
    {
        MenuManager.Instance.EnableRoundMenu(false);
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

    void TimeActive(bool active)
    {
        Time.timeScale = active == true ? 1f : 0f;
    }

    private void ResetStats()
    {
        playerStats.points = 0;
        playerStats.currentHP = playerStats.startHP;
        playerStats.maxHP = playerStats.startHP;
    }

    public void GameReset()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        ResetStats();
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
