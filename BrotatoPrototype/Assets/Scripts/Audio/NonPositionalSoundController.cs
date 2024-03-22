using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class NonPositionalSoundController : MonoBehaviour
{
    [Tooltip("Название блока звуков")]
    [SerializeField] public string soundBlockName;

    [Tooltip("Звуки")]
    [SerializeField] public List<Sound> soundSources;

    [Tooltip("Задержка")]
    [SerializeField] public float delay;

    [Tooltip("Audio Mixer Group")]
    [SerializeField] public AudioMixerGroup audioMixerGroup;

    private float timer = 0;

    private List<AudioSource> soundSourcesList = new List<AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        for (int i = 0; i < soundSourcesList.Count; i++)
        {
            if (!soundSourcesList[i].isPlaying)
            {
                Destroy(soundSourcesList[i]);
                soundSourcesList.RemoveAt(i);
            }
        }
    }

    public void PlaySound()
    {
        if (timer <= 0 && soundSources.Count > 0)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();

            int soundIndex = Random.Range(0, soundSources.Count);

            source.clip = soundSources[soundIndex].clip;
            source.volume = soundSources[soundIndex].volume;
            source.pitch = soundSources[soundIndex].pitch;
            source.loop = soundSources[soundIndex].loop;
            source.outputAudioMixerGroup = audioMixerGroup;

            soundSourcesList.Add(source);
            timer = delay;
            source.Play();
        }
    }
}
