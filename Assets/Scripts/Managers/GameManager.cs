using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;



public class GameManager : Singleton<GameManager>
{
    public UnitStatsSO unitStatsSO;
    public GameObject DontDestroyOnLoadManager;
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
        MenuManager.Instance.EnablePauseMenu(false);
        MenuManager.Instance.EnableShopMenu(false);
        MenuManager.Instance.EnableSettingsMenu(false);
        EnableMenuControls();

        // keep music running
        var dontDestroyManager = GameObject.FindGameObjectWithTag("MusicManager");
        DebugExt.Log(this, $"{dontDestroyManager}");
        if (dontDestroyManager == null)
            Instantiate(DontDestroyOnLoadManager);
    }

    // buttons and menus
    public void OnStarButtonPress() // start button in menu
    {
        ResetStats();
        RoundManager.Instance.StartFirstRound();
        gameStarted = true;
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

    public void OnSettingsBtnPress()
    {
        MenuManager.Instance.EnableMainMenu(false);
        MenuManager.Instance.EnablePauseMenu(false);
        MenuManager.Instance.EnableSettingsMenu(true);
        EnableMenuControls();
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
    }

    public void OnRestartButtonPress()
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

    private void ResetStats()
    {
        unitStatsSO.Reset();
    }

    public void GameReset(bool instant = false)
    {
        if (instant)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            return;
        }

        StartCoroutine(OnPlayerDeathWait());
    }

    IEnumerator OnPlayerDeathWait()
    {
        Debug.Log("Player death wait");
        Camera.main.transform.parent = null;
        GlobalObjectsManager.Instance.player.SetActive(false);

        yield return new WaitForSecondsRealtime(5);
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
        GlobalObjectsManager.Instance.player.GetComponent<PlayerControl>().ResetMoveSpeed();
        _playerInput.SwitchCurrentActionMap(_actionMenuControls);
    }
}
