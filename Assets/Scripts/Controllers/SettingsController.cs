using OneStory.Configs;
using Core.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonobehSingleton<SettingsController>
{
    [SerializeField] private SettingsConfig config;

    public SettingsConfig Config => config;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void ChangeValueMusic(int value)
    {
        config.SettingsVariables.Music = value;
        config.SaveSettings();
    }

    public void ChangeValueSounds(int value)
    {
        config.SettingsVariables.Sounds = value;
        config.SaveSettings();
    }
}
