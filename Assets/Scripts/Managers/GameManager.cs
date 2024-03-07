using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
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
    }

    public void TogglePauseState()
    {
        if (!_gameStarted) return;

        _isPaused = !_isPaused;

        ToggleTimeScale();

        if (_isPaused)
        {
            MenuManager.Instance.MenuOpen();
            EnableMenuControls();
        }
        else
        {
            MenuManager.Instance.MenuClose();
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
            MenuManager.Instance.MenuClose();
            MenuManager.Instance.OnStartPress();
            ToggleTimeScale();
            EnableGameplayControls();
            _gameStarted = true;
        }
        else
        {
            TogglePauseState();
        }
    }

    public void OnQuiButtonPress()
    {
        Application.Quit();
    }
}
