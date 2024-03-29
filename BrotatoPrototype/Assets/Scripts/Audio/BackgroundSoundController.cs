using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundSoundController : MonoBehaviour
{
    [Tooltip("Название блока звуков")]
    [SerializeField] public string soundBlockName;

    [Tooltip("Звуки")]
    [SerializeField] public List<Sound> soundSources;

    [Tooltip("Audio Mixer Group")]
    [SerializeField] public AudioMixerGroup audioMixerGroup;

    private AudioSource movementSource = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound()
    {
        if (movementSource == null)
        {
            int soundIndex = Random.Range(0, soundSources.Count);

            AudioSource source = gameObject.AddComponent<AudioSource>();

            source.clip = soundSources[soundIndex].clip;
            source.volume = soundSources[soundIndex].volume;
            source.pitch = soundSources[soundIndex].pitch;
            source.loop = soundSources[soundIndex].loop;
            source.outputAudioMixerGroup = audioMixerGroup;

            movementSource = source;
            source.Play();
        }
    }

    public void StopSound()
    {
        if (soundSources != null)
        {
            movementSource.Stop();
            Destroy(movementSource);
            movementSource = null;
        }
    }
}
