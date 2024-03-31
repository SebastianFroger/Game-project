using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Shooter;
using Unity.VisualScripting;



public class GameManager : Singleton<GameManager>
{
    public UnitStatsSO unitStatsSO;
    public GameObject DontDestroyOnLoadManager;
    private bool _isPaused;
    public bool gameStarted;
    public PlayerInput _playerInput;
    private string _actionMapPlayerControls = "Player Control";
    private string _actionMenuControls = "Menu Control";
    private bool _inSettingsMenu;
    private bool _inConfigsMenu;

    void Start()
    {
        gameStarted = false;
        TimeActive(false);
        MenuManager.Instance.EnableMainMenu(true);
        MenuManager.Instance.EnableGameUI(false);
        MenuManager.Instance.EnablePauseMenu(false);
        MenuManager.Instance.EnableShopMenu(false);
        MenuManager.Instance.EnableSettingsMenu(false);
        MenuManager.Instance.EnableConfigurationsMenu(false);
        EnableMenuControls();

        // keep music running
        var dontDestroyManager = GameObject.FindGameObjectWithTag("MusicManager");
        if (dontDestroyManager == null)
            Instantiate(DontDestroyOnLoadManager);
    }

    // buttons and menus
    public void OnStarButtonPress() // start button in main menu
    {
        MenuManager.Instance.EnableMainMenu(false);
        MenuManager.Instance.EnableConfigurationsMenu(true);
        EnvironmentSpawner.Instance.SpawnEnvironment();
        StatsManager.Instance.ResetStats();
        gameStarted = true;
        _inConfigsMenu = true;
    }

    public void OnConfigStarButtonPress() // start button config in menu
    {
        ConfigurationController.Instance.ApplyConfigValues();
        MenuManager.Instance.EnableConfigurationsMenu(false);
        RoundManager.Instance.StartFirstRound();
        _inConfigsMenu = false;
        GlobalObjectsManager.Instance.player.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void OnResumeButtonPress() // resume button in menu
    {
        PauseMenuToggle();
    }

    public void PauseMenuToggle()
    {
        if (!gameStarted) return;

        if (_inSettingsMenu)
        {
            OnSettingsBackBtnPress();
            return;
        }

        _isPaused = !_isPaused;

        if (_isPaused)
        {
            TimeActive(false);
            MenuManager.Instance.EnableGameUI(false);
            MenuManager.Instance.EnableConfigurationsMenu(false);
            MenuManager.Instance.EnablePauseMenu(true);
            EnableMenuControls();
        }
        else
        {
            if (_inConfigsMenu)
            {
                MenuManager.Instance.EnableConfigurationsMenu(true);
            }
            else
            {
                MenuManager.Instance.EnableConfigurationsMenu(false);
                MenuManager.Instance.EnableGameUI(true);
                EnableGameplayControls();
                TimeActive(true);
            }

            MenuManager.Instance.EnablePauseMenu(false);
        }

    }

    public void OnSettingsBtnPress()
    {
        MenuManager.Instance.EnableMainMenu(false);
        MenuManager.Instance.EnablePauseMenu(false);
        MenuManager.Instance.EnableSettingsMenu(true);
        EnableMenuControls();
        _inSettingsMenu = true;
    }

    public void OnSettingsBackBtnPress()
    {
        MenuManager.Instance.EnableSettingsMenu(false);
        if (!gameStarted)
        {
            MenuManager.Instance.EnableMainMenu(true);
            return;
        }
        MenuManager.Instance.EnablePauseMenu(true);
        _inSettingsMenu = false;
    }

    public void OnRestartButtonPress() // restart game
    {
        GameReset(true);
    }

    public void OnQuitAppButtonPress()
    {
        Application.Quit();
    }

    public void TimeActive(bool active)
    {
        Time.timeScale = active == true ? 1f : 0f;
    }

    public void GameReset(bool instant = false)
    {

        if (instant)    // restrt game
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
            return;
        }

        // when player dies wait 5 seconds before restarting
        StartCoroutine(OnPlayerDeathWait());
        RoundManager.Instance.StopRoundTime();
    }

    IEnumerator OnPlayerDeathWait()
    {
        Camera.main.transform.parent = null;
        GlobalObjectsManager.Instance.player.SetActive(false);

        yield return new WaitForSecondsRealtime(5);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }

    // actioncontrols
    public void EnableGameplayControls()
    {
        _playerInput.SwitchCurrentActionMap(_actionMapPlayerControls);
    }

    public void EnableMenuControls()
    {
        GlobalObjectsManager.Instance.player.GetComponent<PlayerControl>().ResetMoveSpeed();
        _playerInput.SwitchCurrentActionMap(_actionMenuControls);
    }
}
