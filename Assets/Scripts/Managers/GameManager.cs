using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class GameManager : Singleton<GameManager>
{
    public PlayerStats playerStats;
    private bool _isPaused;
    private bool _gameStarted;
    public PlayerInput _playerInput;
    private string _actionMapPlayerControls = "Player Controls";
    private string _actionMenuControls = "Menu Controls";

    void Start()
    {
        _gameStarted = false;
        Time.timeScale = 0f; // stop time at startmenu
        EnableMenuControls();
        MenuManager.Instance.ShowGameUI(false);
    }

    public void GameReset()
    {
        Debug.Log("GameReset");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void TogglePauseState()
    {
        if (!_gameStarted) return;

        _isPaused = !_isPaused;

        ToggleTimeScale();

        if (_isPaused)
        {
            MenuManager.Instance.ShowGameUI(false);
            MenuManager.Instance.MenuOpen(true);
            EnableMenuControls();
        }
        else
        {
            MenuManager.Instance.ShowGameUI(true);
            MenuManager.Instance.MenuOpen(false);
            EnableGameplayControls();
        }
    }

    void ToggleTimeScale()
    {
        if (_isPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
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

    public void OnStartResumeButtonPress()
    {
        if (!_gameStarted)
        {
            MenuManager.Instance.MenuOpen(false);
            MenuManager.Instance.OnStartPress();
            ToggleTimeScale();
            EnableGameplayControls();
            ResetStats();
            _gameStarted = true;
        }
        else
        {
            TogglePauseState();
        }
    }

    public void OnQuiButtonPress()
    {
        MenuManager.Instance.OnQuitPress();
    }

    private void ResetStats()
    {
        playerStats.points = 0;
        playerStats.currentHP = playerStats.startHP;
        playerStats.maxHP = playerStats.startHP;
    }


}
