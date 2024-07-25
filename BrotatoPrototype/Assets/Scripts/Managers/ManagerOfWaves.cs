using System.Collections.Generic;
using UnityEngine;

public class ManagerOfWaves : MonoBehaviour
{
    [SerializeField] private ManagerEnemyTier _managerEnemyTier;
    [SerializeField] private List<WaveSetting> _listOfWaveSettings;

    private void CreateWave(WaveSetting waveSetting)
    {
        GameObject wavePrefab = new GameObject("newWave");
        GameObject waveObj = Instantiate(wavePrefab, transform);
        waveObj.transform.parent = transform;
        WaveController wave = waveObj.AddComponent<WaveController>();
        waveSetting.Wave = wave;
        wave.time = waveSetting.parametersOfWave.waveTime;
        wave.SetWaveSettings(waveSetting.enemySpawnerSettings, waveSetting.parametersOfWave.amountOfGoldPerWave, waveSetting.parametersOfWave.amountOfExpPerWave, _managerEnemyTier);
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
