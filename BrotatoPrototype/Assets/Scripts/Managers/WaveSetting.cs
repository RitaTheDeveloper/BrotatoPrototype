using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSetting : MonoBehaviour
{
    [SerializeField] public float _waveTime = 30f;
    [SerializeField] private int _amountOfGoldPerWave = 100;
    [SerializeField] private EnemyStrengthFactors _enemyStrengthFactors = new EnemyStrengthFactors(1f, 1f, 1f);
    [SerializeField] private List<EnemySpawnerSettings> enemySpawnerSettings;

    public WaveController wave;
  
    public void CreateWave()
    {
        GameObject waveObj = Instantiate(new GameObject("newWave"), transform);
        waveObj.transform.parent = transform;
        wave = waveObj.AddComponent<WaveController>();
        wave.time = _waveTime;
        wave.SetEnemySpawnerSettings(enemySpawnerSettings);      
    }

    // расчитать интервал и общее кол-во врагов и передать enemySpawner

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

    [System.Serializable]
    public class EnemySpawnerSettings
    {
        [Header("префаб моба")]
        public EnemyController enemy;
        [Header("кд спавна")]
        public float spawnCd = 5;
        [Header("общее кол-во мобов за волну")]
        public int totalAmountOfEnemies = 20;
        [Header("кол-во мобов в пачке")]
        public int amountOfEnemiesInPack = 1;
        [Header("задержка перед первым спавном")]
        public float startSpawnTime;
        [Header("время до конца волна, чтобы закончить спавнить")]
        public float endSpawnTime;                

        public float GetCdSpawn(float timeOfWave)
        {            
            float result = (timeOfWave - startSpawnTime - endSpawnTime) / ((float)GetTotalAmountOfEnemies() / (float)amountOfEnemiesInPack);
            return result;
        }

        public int GetTotalAmountOfEnemies()
        {
            return totalAmountOfEnemies - totalAmountOfEnemies % amountOfEnemiesInPack;
        }

        public int GetTotalAmountOfEnemies(float timeOfWave)
        {
            return (int)((timeOfWave - startSpawnTime - endSpawnTime) / spawnCd * (float)amountOfEnemiesInPack);
        }

    }
   
}
