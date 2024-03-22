using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Tooltip("Audio Mixer Master")]
    [SerializeField] public AudioMixerGroup masterAudioGroup;

    [Tooltip("Контроллеры звука непозиционные")]
    [SerializeField] public List<NonPositionalSoundController> NonPositionalSoundControllers;

    [Tooltip("Контроллеры звука позиционные")]
    [SerializeField] public List<PositionalSoundController> PositionalSoundControllers;

    private Dictionary<string, NonPositionalSoundController> NonPositionalSounds = new Dictionary<string, NonPositionalSoundController>();
    private Dictionary<string, PositionalSoundController> PositionalSounds = new Dictionary<string, PositionalSoundController>();

    private void Awake()
    {
        instance = this;
        
        for (int i = 0; i < NonPositionalSoundControllers.Count; i++)
        {
            NonPositionalSounds[NonPositionalSoundControllers[i].soundBlockName] = NonPositionalSoundControllers[i];
        }
        for (int i = 0; i < PositionalSoundControllers.Count; i++)
        {
            PositionalSounds[PositionalSoundControllers[i].soundBlockName] = PositionalSoundControllers[i];
        }
    }

    public void Play(string name)
    {
        if (NonPositionalSounds.ContainsKey(name))
        {
            NonPositionalSounds[name].PlaySound();
        }
    }
    
    public void Play(string name, Vector3 position)
    {
        if (PositionalSounds.ContainsKey(name))
        {
            PositionalSounds[name].PlaySound(position);
        }
    }
}

    

