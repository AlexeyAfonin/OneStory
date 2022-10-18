using Core.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameContoller : MonobehSingleton<GameContoller>
{
    [Header("Canvas")]
    [SerializeField] private Canvas mainCanvas;
    [Header("Windows")]
    [SerializeField] private GameObject pauseWindow;
    [Header("Buttons")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    private bool _isGamePause = false;

    public bool IsGamePause => _isGamePause;


    protected override void Awake()
    {
        base.Awake();
        continueButton.onClick.AddListener(ContinueGame);
        settingsButton.onClick.AddListener(ShowSettings);
        exitButton.onClick.AddListener(ExitInMainMenu);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        LockMouse();
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void ShowSettings()
    {
        SettingsController.Instance.ShowWindow(mainCanvas);
    }

    private void ExitInMainMenu()
    {
        Time.timeScale = 1f;
        UnlockMouse();
        _isGamePause = false;
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        pauseWindow.SetActive(true);
        _isGamePause = true;
        Time.timeScale = 0f;
        UnlockMouse();
    }

    public void ContinueGame()
    {
        pauseWindow.SetActive(false);
        Time.timeScale = 1f;
        _isGamePause = false;
        LockMouse();
    }
}
