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
        PlayerPrefs.SetInt("WaveEnded1", data.WaveEnded);
        PlayerPrefs.SetFloat("MasterSoundVolume", data.MasterSoundVolume);
        PlayerPrefs.SetFloat("MusicSondVolume", data.MusicSondVolume);
        PlayerPrefs.SetFloat("SFXVolume", data.SFXVolume);

        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        SaveData data_tmp = new SaveData();

        data_tmp.WaveEnded = PlayerPrefs.GetInt("WaveEnded1", 0);
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

    public void ResetData()
    {
        data.WaveEnded = 0;
        PlayerPrefs.SetInt("WaveEnded1", 0);
    }

    public List<GameObject> GetUnlockCharacterList(SaveData new_data)
    {
        List<bool> openCharacters = new List<bool>();
        for (int i = 0; i < GameManager.instance.PlayerPrefabs.Length; i++)
        {
            openCharacters.Add(GameManager.instance.PlayerPrefabs[i].GetComponent<UnlockCharacterComponent>().UnlockCharacter());
        }

        this.SetData(new_data);
        this.SaveData();

        List<GameObject> result = new List<GameObject>();

        for (int i = 0; i < GameManager.instance.PlayerPrefabs.Length; i++)
        {
            if (GameManager.instance.PlayerPrefabs[i].GetComponent<UnlockCharacterComponent>().UnlockCharacter() && openCharacters[i] == false)
            {
                result.Add(GameManager.instance.PlayerPrefabs[i]);
            }
        }
        return result;
    }
}
