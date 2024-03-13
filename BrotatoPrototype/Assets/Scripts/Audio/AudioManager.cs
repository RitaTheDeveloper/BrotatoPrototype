using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] soundsEveryWhere;
    public Sound[] soundsPositionMatter;

    private void Awake()
    {
        instance = this;

        foreach (Sound s in soundsEveryWhere)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(soundsEveryWhere, sound => sound.soundName == name);

        if (s == null)
        {
            Debug.LogError("Sound " + name + " not found!");
            return;
        }

        s.audioSource.PlayOneShot(s.clip);
    }

    public AudioClip GetAudioClip(string name)
    {
        foreach (Sound s in soundsPositionMatter)
        {
            if (s.soundName.Equals(name))
            {
                return s.clip;
            }
        }

        Debug.LogError("Sound " + name + " not found!");
        return null;
    }
    
}

    

