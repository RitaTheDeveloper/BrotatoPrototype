using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private SaveData data;

    private void Awake()
    {
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
        data = _data;
    }

    public SaveData GetData()
    {
        return data;
    }

    public void ResetData()
    {
        //data.WaveEnded = 0;
        //PlayerPrefs.SetInt("WaveEnded", 0);
        PlayerPrefs.DeleteAll();
    }

    public List<GameObject> GetUnlockCharacterList(SaveData new_data)
    {
        List<bool> openCharacters = new List<bool>();
        for (int i = 0; i < GameManager.instance.PlayerPrefabs.Length; i++)
        {
            openCharacters.Add(GameManager.instance.PlayerPrefabs[i].GetComponent<UnlockCharacterComponent>().UnlockCharacter());
        }

        SetData(new_data);
        SaveData();

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

    public void SaveCharacterLvl(string name, int currentlvl)
    {
        string key = GetNameCharacter(name) + "_lvl";
        Debug.Log(key);
        PlayerPrefs.SetInt(key, currentlvl);
        PlayerPrefs.Save();
    }

    public int GetCharacterLvl(string name)
    {
        string key = GetNameCharacter(name) + "_lvl";
        Debug.Log(key);
        return PlayerPrefs.GetInt(key);
    }

    public void SaveCharacterWaveCount(string name, int currentWaveCount)
    {
        string key = GetNameCharacter(name) + "_waveCount";
        PlayerPrefs.SetInt(key, currentWaveCount);
        PlayerPrefs.Save();
    }

    public int GetCharacterWaveCount(string name)
    {
        string key = GetNameCharacter(name) + "_waveCount";
        return PlayerPrefs.GetInt(key);
    }

    private string GetNameCharacter(string name)
    {
        return name.Replace("(Clone)", "");
    }

}
