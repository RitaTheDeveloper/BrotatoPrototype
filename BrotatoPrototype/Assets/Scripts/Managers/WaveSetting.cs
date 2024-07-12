using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveSetting
{
    public string name;
    public ParametersOfWave parametersOfWave = new ParametersOfWave(30, 100, 100);
    public EnemyStrengthFactors _enemyStrengthFactors = new EnemyStrengthFactors(1f, 1f, 1f);
    public List<EnemySpawnerSettings> enemySpawnerSettings = new List<EnemySpawnerSettings>();

    private WaveController wave;

    public EnemyStrengthFactors GetEnemyStrengthFactors { get => _enemyStrengthFactors;}
    public WaveController Wave { get => wave; set => wave = value; }    
}

