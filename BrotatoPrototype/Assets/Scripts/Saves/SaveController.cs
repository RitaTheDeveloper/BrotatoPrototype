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

        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        SaveData data_tmp = new SaveData();

        data_tmp.WaveEnded = PlayerPrefs.GetInt("WaveEnded", 0);

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
