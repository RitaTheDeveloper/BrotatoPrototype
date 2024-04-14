using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    static SaveController instance;

    private SaveData data;

    private void Awake()
    {
        instance = this;
        LoadData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("WaveEnded", data.WaveEnded);
        PlayerPrefs.SetFloat("MasterSoundVolume", data.MasterSoundVolume);
        PlayerPrefs.SetFloat("MusicSondVolume", data.MusicSondVolume);
        PlayerPrefs.SetFloat("SFXVolume", data.SFXVolume);

        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        SaveData data_tmp = new SaveData();

        data_tmp.WaveEnded = PlayerPrefs.GetInt("WaveEnded", 0);
        data_tmp.MasterSoundVolume = PlayerPrefs.GetFloat("MasterSoundVolume", 0);
        data_tmp.MusicSondVolume = PlayerPrefs.GetFloat("MusicSondVolume", 0);
        data_tmp.SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0);

        data = data_tmp;
    }

    public void SetData(SaveData _data)
    {
        this.data = _data;
    }

    public SaveData GetData()
    {
        return data;
    }
}