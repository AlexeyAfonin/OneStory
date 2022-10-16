using OneStory.Configs;
using OneStory.Core.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonobehSingleton<SettingsController>
{
    [SerializeField] private SettingsConfig config;
    [Header("Buttons")]
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;

    public SettingsConfig Config => config;

    protected override void Awake()
    {
        base.Awake();

        if(saveButton is not null) saveButton.onClick.AddListener(SaveSettings);
        if(loadButton is not null) loadButton.onClick.AddListener(LoadSettings);
    }

    private void SaveSettings()
    {
        Config.SaveSettings();
    }

    private void LoadSettings()
    {
        Config.LoadSettings();
    }
}
