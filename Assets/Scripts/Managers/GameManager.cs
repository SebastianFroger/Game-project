using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    private bool _isPaused;
    private bool _gameStarted;
    private PlayerInput _playerInput;
    private string _actionMapPlayerControls = "Player Controls";
    private string _actionMenuControls = "Menu Controls";

    void Start()
    {
        _isPaused = false;
        _gameStarted = false;
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
}
