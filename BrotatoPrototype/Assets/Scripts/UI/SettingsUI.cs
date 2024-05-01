using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI instance;

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

    private void SetMasterVolume()
    {
        MasterAudioMixer.audioMixer.SetFloat("MasterVulomeParam", MasterSlider.value);
    }

    private void SetMusicVolume()
    {
        MusicAudioMixer.audioMixer.SetFloat("BackGroundMusicVolumeParam", MusicSlider.value);
    }

    private void SetSFXVolume()
    {
        SFXAudioMixer.audioMixer.SetFloat("SFXVolumeParam", SFXSlider.value);
    }

    private void Awake()
    {
        instance = this;


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
        SaveController saveController = gameObject.AddComponent<SaveController>();
        saveController.LoadData();
        SaveData saveData = saveController.GetData();
        Destroy(saveController);
        MasterSlider.value = saveData.MasterSoundVolume;
        MusicSlider.value = saveData.MusicSondVolume;
        SFXSlider.value = saveData.SFXVolume;

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
        SaveController saveController = gameObject.AddComponent<SaveController>();
        saveController.LoadData();
        SaveData saveData = saveController.GetData();

        saveData.MasterSoundVolume = MasterSlider.value;
        saveData.MusicSondVolume = MusicSlider.value;
        saveData.SFXVolume = SFXSlider.value;

        saveController.SetData(saveData);
        saveController.SaveData();

        Destroy(saveController);
        gameObject.SetActive(false);
    }
}
