using Core.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonobehSingleton<MainMenuController>
{
    [SerializeField] private Canvas mainCanvas;

    public Canvas MainCanvas => mainCanvas;

    public void Play() =>
        SceneLoader.Instance.LoadScene("Game");

    public void Settings() =>
        SettingsController.Instance.ShowWindow(mainCanvas);

    public void Quit() =>
        Application.Quit();
}
