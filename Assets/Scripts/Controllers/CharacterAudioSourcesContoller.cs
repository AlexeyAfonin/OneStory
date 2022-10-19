using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OneStory.Core.Utils.Enums;

public class CharacterAudioSourcesContoller : MonoBehaviour
{
    [SerializeField] protected AudioClip soundWalk;
    [SerializeField] protected AudioClip soundAttack;
    [SerializeField] protected List<AudioClip> soundsHit;
    [Space(3f)]
    [SerializeField] protected AudioSource audioSourceWalk;
    [SerializeField] protected AudioSource audioSourceAttack;
    [SerializeField] protected AudioSource audioSourceHit;

    private void Start()
    {
        SoundsController.Instance.EffectsAudioSources.Add(audioSourceWalk);
        SoundsController.Instance.EffectsAudioSources.Add(audioSourceAttack);
        SoundsController.Instance.EffectsAudioSources.Add(audioSourceHit);
    }

    public void PlaySound(TypeSoundEffect sound, bool loop = false)
    {
        AudioClip audioClip;
        AudioSource audioSource;

        switch (sound)
        {
            case TypeSoundEffect.Walk:
                audioClip = soundWalk;
                audioSource = audioSourceWalk;
                break;
            case TypeSoundEffect.Attack:
                audioClip = soundAttack;
                audioSource = audioSourceAttack;
                break;
            case TypeSoundEffect.Hit:
                audioClip = soundsHit[Random.Range(0, soundsHit.Count - 1)];
                audioSource = audioSourceAttack;
                break;
            case TypeSoundEffect.None:
            default:
                return;
        }

        audioSource.clip = audioClip;
        audioSource.loop = loop;
        audioSource.volume = SettingsController.Instance.Config.SettingsVariables.Sounds / 100f;
        audioSource.Play();
    }

    public void StopSound(TypeSoundEffect sound)
    {
        AudioSource audioSource;

        switch (sound)
        {
            case TypeSoundEffect.Walk:
                audioSource = audioSourceWalk;
                break;
            case TypeSoundEffect.Attack:
                audioSource = audioSourceAttack;
                break;
            case TypeSoundEffect.None:
            default:
                return;
        }

        audioSource.Stop();
    }
}
