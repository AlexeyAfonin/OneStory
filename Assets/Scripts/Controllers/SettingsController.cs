using OneStory.Configs;
using Core.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonobehSingleton<SettingsController>
{
    [SerializeField] private SettingsConfig config;
    [Space(10f), Header("UI")]
    [SerializeField] private GameObject windowPrefab;

    private SettingsWindow _windowOnScene;

    public SettingsConfig Config => config;
    public GameObject Window => windowPrefab;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void ShowWindow(Canvas canvas)
    {
        if (_windowOnScene == null)
        {
            InitWindow(canvas);
        }
        else
        {
            _windowOnScene.Show();
        }
    }

    private void InitWindow(Canvas canvas)
    {
        var window = Instantiate(windowPrefab, canvas.transform);
        _windowOnScene = window.GetComponent<SettingsWindow>();
        _windowOnScene.Show();
    }
}
