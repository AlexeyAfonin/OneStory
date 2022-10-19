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

    public override void Show()
    {
        base.Show();

        var config = SettingsController.Instance.Config;
        var settings = config.SettingsVariables;

        musicSlider.value = settings.Music;
        soundsSlider.value = settings.Sounds;
    }

    public override void Hide()
    {
        ChangeVolumeSounds();
        ChangeVolumeMusic();
        base.Hide();
    }

    public void ChangeVolumeSounds()
    {
        var config = SettingsController.Instance.Config;
        config.SettingsVariables.Sounds = (int)soundsSlider.value;
        config.SaveSettings();
        SoundsController.Instance.ChangeVolume();
    }

    public void ChangeVolumeMusic()
    {
        var config = SettingsController.Instance.Config;
        config.SettingsVariables.Music = (int)musicSlider.value;
        config.SaveSettings();
        SoundsController.Instance.ChangeVolume();
    }
}
