using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveSetting
{
    public string name;
    //public float _waveTime = 30f;
    //public int _amountOfGoldPerWave = 100;
    //public int _amountOfExpPerWave = 100;
    public ParametersOfWave parametersOfWave = new ParametersOfWave(30, 100, 100);
    public EnemyStrengthFactors _enemyStrengthFactors = new EnemyStrengthFactors(1f, 1f, 1f);
    public List<EnemySpawnerSettings> enemySpawnerSettings = new List<EnemySpawnerSettings>();

    private WaveController wave;

    public EnemyStrengthFactors GetEnemyStrengthFactors { get => _enemyStrengthFactors;}
    public WaveController Wave { get => wave; set => wave = value; }    
}

//[System.Serializable]
//public class ParametersOfWave
//{
//    public float waveTime = 30f;
//    public int amountOfGoldPerWave = 100;
//    public int amountOfExpPerWave = 100;

//    public ParametersOfWave(float time, int amountOfGold, int amountOfExp)
//    {
//        waveTime = time;
//        amountOfGoldPerWave = amountOfGold;
//        amountOfExpPerWave = amountOfExp;
//    }
//}
