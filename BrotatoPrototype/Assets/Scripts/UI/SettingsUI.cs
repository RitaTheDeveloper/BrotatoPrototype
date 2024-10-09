using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    //public static SettingsUI instance;

    [Header("Общая громкость микшер")]
    [SerializeField] public AudioMixerGroup MasterAudioMixer;
    [Header("Общая громкость Slider")]
    [SerializeField] public Slider MasterSlider;

    [Header("Громкость музыки микшер")]
    [SerializeField] public AudioMixerGroup MusicAudioMixer;
    [Header("Громкость музыки slider")]
    [SerializeField] public Slider MusicSlider;

    [Header("Громкость звуков микшер")]
    [SerializeField] public AudioMixerGroup SFXAudioMixer;
    [Header("Громкость звуков slider")]
    [SerializeField] public Slider SFXSlider;

    private SaveController _saveController;

    private void SetMasterVolume()
    {
        float value = MasterSlider.value;
        if (value < -40)
        {
            value = -80f;
        }
        MasterAudioMixer.audioMixer.SetFloat("MasterVulomeParam", value);
    }

    private void SetMusicVolume()
    {
        float value = MusicSlider.value;
        if (value < -40)
        {
            value = -80f;
        }
        MusicAudioMixer.audioMixer.SetFloat("BackGroundMusicVolumeParam", value);
    }

    private void SetSFXVolume()
    {
        float value = SFXSlider.value;
        if (value < -40)
        {
            value = -80f;
        }
        SFXAudioMixer.audioMixer.SetFloat("SFXVolumeParam", value);
    }

    private void Awake()
    {
        _saveController = GetComponent<SaveController>();
        MasterSlider.onValueChanged.AddListener(delegate { SetMasterVolume(); });
        MusicSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
        SFXSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });
    }

    // Start is called before the first frame update
    void Start()
    {
        InitSoundsVolume();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitSoundsVolume()
    {
        
        _saveController.LoadData();
        SaveData saveData = _saveController.GetData();

        if (saveData.MasterSoundVolume < -40)
        {
            MasterSlider.value = -40f;
        }
        else
        {
            MasterSlider.value = saveData.MasterSoundVolume;
        }

        if (saveData.MasterSoundVolume < -40)
        {
            MusicSlider.value = -40f;
        }
        else
        {
            MusicSlider.value = saveData.MusicSondVolume;
        }

        if (saveData.MasterSoundVolume < -40)
        {
            SFXSlider.value = -40f;
        }
        else
        {
            SFXSlider.value = saveData.SFXVolume;
        }

        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    private void OnDestroy()
    {
        CloseSettings();
    }

    public void CloseSettings()
    {
        _saveController.LoadData();
        SaveData saveData = _saveController.GetData();

        float value = MasterSlider.value;
        if (value < -40)
        {
            value = -80f;
        }
        saveData.MasterSoundVolume = value;

        value = MusicSlider.value;
        if (value < -40)
        {
            value = -80f;
        }
        saveData.MusicSondVolume = value;

        value = SFXSlider.value;
        if (value < -40)
        {
            value = -80f;
        }
        saveData.SFXVolume = value;

        _saveController.SetData(saveData);
        _saveController.SaveData();
        PlaySoundOfButtonPress();
        gameObject.SetActive(false);
    }

    private void PlaySoundOfButtonPress()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("ClickElement");
        }
    }
}
