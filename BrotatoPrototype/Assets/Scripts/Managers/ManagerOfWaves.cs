using System.Collections.Generic;
using UnityEngine;

public class ManagerOfWaves : MonoBehaviour
{
    [SerializeField] private List<WaveSetting> _listOfWaveSettings;

    private void CreateWave(WaveSetting waveSetting)
    {
        GameObject wavePrefab = new GameObject("newWave");
        GameObject waveObj = Instantiate(wavePrefab, transform);
        waveObj.transform.parent = transform;
        WaveController wave = waveObj.AddComponent<WaveController>();
        waveSetting.Wave = wave;
        wave.time = waveSetting._waveTime;
        wave.SetWaveSettings(waveSetting.enemySpawnerSettings, waveSetting._amountOfGoldPerWave, waveSetting._amountOfExpPerWave);
        Destroy(wavePrefab);
    }

    public void CreateWaves()
    {
        foreach(WaveSetting ws in _listOfWaveSettings)
        {
            CreateWave(ws);
        }
    }

    public List<WaveSetting> GetListOfWaveSettings()
    {
        return _listOfWaveSettings;
    }
}
