using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController2 : MonoBehaviour
{
    [SerializeField] private float _waveTime = 30f;
    [SerializeField] private int _amountOfGoldPerWave = 100;
    [SerializeField] private EnemyStrengthFactors _enemyStrengthFactors = new EnemyStrengthFactors(1f, 1f, 1f);
    [SerializeField] private List<EnemySpawnerSettings> enemySpawnerSettings;

    public WaveController wave;
    private void Awake()
    {
        //CreateWave();
    }
    public void CreateWave()
    {
        //WaveController[] waves;
        GameObject waveObj = Instantiate(new GameObject("newWave"), transform);
        wave = waveObj.AddComponent<WaveController>();
        wave.time = _waveTime;
        wave.SetEnemySpawnerSettings(enemySpawnerSettings);
        //wave.enemySpawnersPrefabs = 
       // GameObject enemySpawner1Obj = Instantiate(new GameObject("newEnemySpawner"), wave.transform);
        //EnemySpawner enemySpawner1 = enemySpawner1Obj.AddComponent<EnemySpawner>();
       // enemySpawner1.SetEnemy(enemySpawnerSettings.enemy);
        //List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
        //enemySpawners.Add(enemySpawner1);
        //wave.enemySpawnersPrefabs = enemySpawners;        
    }

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
    public struct EnemySpawnerSettings
    {
        public EnemyController enemy;
        public float spawnCd;
        private EnemySpawner enemySpawner;
        
        public EnemySpawnerSettings(EnemyController enemy, float spawnCd)
             : this()
        {
            this.enemy = enemy;
            this.spawnCd = spawnCd;
        }                
    }

    




}
