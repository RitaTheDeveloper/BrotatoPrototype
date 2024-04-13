using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI instance;

    [Header("����� ��������� ������")]
    [SerializeField] public AudioMixerGroup MasterAudioMixer;
    [Header("����� ��������� Slider")]
    [SerializeField] public Slider MasterSlider;

    [Header("��������� ������ ������")]
    [SerializeField] public AudioMixerGroup MusicAudioMixer;
    [Header("��������� ������ slider")]
    [SerializeField] public Slider MusicSlider;

    [Header("��������� ������ ������")]
    [SerializeField] public AudioMixerGroup SFXAudioMixer;
    [Header("��������� ������ slider")]
    [SerializeField] public Slider SFXSlider;

    private void SetMasterVolume()
    {
        MasterAudioMixer.audioMixer.SetFloat(MasterAudioMixer.name, MasterSlider.value);
    }

    private void SetMusicVolume()
    {
        MusicAudioMixer.audioMixer.SetFloat(MusicAudioMixer.name, MusicSlider.value);
    }

    private void SetSFXVolume()
    {
        SFXAudioMixer.audioMixer.SetFloat(SFXAudioMixer.name, SFXSlider.value);
    }

    private void Awake()
    {
        instance = this;

        InitSoundsVolume();

        MasterSlider.onValueChanged.AddListener(delegate { SetMasterVolume(); });
        MusicSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
        MusicSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });
    }

    // Start is called before the first frame update
    void Start()
    {

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

        Destroy(saveController);
    }
}
