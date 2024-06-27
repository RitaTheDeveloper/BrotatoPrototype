using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveSetting
{
    public string name;
    public float _waveTime = 30f;
    public int _amountOfGoldPerWave = 100;
    public int _amountOfExpPerWave = 100;
    public EnemyStrengthFactors _enemyStrengthFactors = new EnemyStrengthFactors(1f, 1f, 1f);
    public List<EnemySpawnerSettings> enemySpawnerSettings;

    private WaveController wave;

    public EnemyStrengthFactors GetEnemyStrengthFactors { get => _enemyStrengthFactors;}
    public WaveController Wave { get => wave; set => wave = value; }    

    [System.Serializable]
    public struct EnemyStrengthFactors
    {
        public float speedFactor;
        public float damageFactor;
        public float healthFactor;

        public EnemyStrengthFactors(float speedFactor, float damageFactor, float healthFactor)
            : this()
        {
            this.speedFactor = speedFactor;
            this.damageFactor = damageFactor;
            this.healthFactor = healthFactor;
        }
    }
   
}
