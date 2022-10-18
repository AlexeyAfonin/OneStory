using Core.Base;
using OneStory.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : BaseWindow<SettingsWindow>
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundsSlider;


    private SettingsConfig _config;
    private SettingsVariables _settings;

    public override void Show()
    {
        base.Show();
        _config = SettingsController.Instance.Config;
        _config.LoadSettings();
        _settings = _config.SettingsVariables;
        musicSlider.value = _settings.Music;
        soundsSlider.value = _settings.Sounds;
    }

    public override void Hide()
    {
        _config = SettingsController.Instance.Config;
        _config.SettingsVariables.Music = (int)musicSlider.value;
        _config.SettingsVariables.Sounds = (int)soundsSlider.value;
        _config.SaveSettings();
        base.Hide();
    }
}
