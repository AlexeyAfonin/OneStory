using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utils;

public class SoundsController : MonobehSingleton<SoundsController>
{
    public AudioSource BackgroundSound;
    public AudioSource BackgroundMusic;
    public List<AudioSource> EffectsAudioSources;

    private void Start()
    {
        ChangeSoundVolume();
        ChangeMusicVolume();
    }

    public void ChangeSoundVolume()
    {
        var settings = SettingsController.Instance.Config.SettingsVariables;

        if (BackgroundSound != null)
        {
            BackgroundSound.volume = settings.Sounds / 100f;
        }

        foreach (var effect in EffectsAudioSources)
        {
            effect.volume = settings.Sounds / 100f;
        }
    }

    public void ChangeMusicVolume()
    {
        var settings = SettingsController.Instance.Config.SettingsVariables;

        if (BackgroundMusic != null)
        {
            BackgroundMusic.volume = settings.Music / 100f;
        }
    }
}
