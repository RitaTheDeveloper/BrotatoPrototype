using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveSetting
{
    public float _waveTime = 30f;
    public int _amountOfGoldPerWave = 100;
    public int _amountOfExpPerWave = 100;
    public EnemyStrengthFactors _enemyStrengthFactors = new EnemyStrengthFactors(1f, 1f, 1f);
    public List<EnemySpawnerSettings> enemySpawnerSettings;

    private WaveController wave;

    public EnemyStrengthFactors GetEnemyStrengthFactors { get => _enemyStrengthFactors;}
    public WaveController Wave { get => wave; set => wave = value; }

    //public WaveController Wave { get => wave; set}

    //public void CreateWave()
    //{
    //    GameObject wavePrefab = new GameObject("newWave");
    //    GameObject waveObj = Instantiate(wavePrefab, transform);
    //    waveObj.transform.parent = transform;
    //    wave = waveObj.AddComponent<WaveController>();
    //    wave.time = _waveTime;
    //    wave.SetWaveSettings(enemySpawnerSettings, _amountOfGoldPerWave, _amountOfExpPerWave);
    //    Destroy(wavePrefab);
    //}    

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

   //[System.Serializable]
   // public class EnemySpawnerSettings
   // {
   //     [Header("������ ����")]
   //     public EnemyController enemy;
   //     [Header("�� ������")]
   //     public float spawnCd = 5;
   //     [Header("����� ���-�� ����� �� �����")]
   //     public int totalAmountOfEnemies = 20;
   //     [Header("���-�� ����� � �����")]
   //     public int amountOfEnemiesInPack = 1;
   //     [Header("�������� ����� ������ �������")]
   //     public float startSpawnTime;
   //     [Header("����� �� ����� �����, ����� ��������� ��������")]
   //     public float endSpawnTime;
   //     [Header("��� ������ - � ���������� ����� ��� � ������� �� ������")]
   //     public SpawnPointType spawnPointType;

   //     public float GetCdSpawn(float timeOfWave)
   //     {
   //         float result = (timeOfWave - startSpawnTime - endSpawnTime) / ((float)GetTotalAmountOfEnemies() / (float)amountOfEnemiesInPack);
   //         Debug.Log("cd= " + result);
   //         return result;
   //     }

   //     public int GetTotalAmountOfEnemies()
   //     {
   //         Debug.Log("totalAmForEnemySpawner " + (totalAmountOfEnemies - totalAmountOfEnemies % amountOfEnemiesInPack));
   //         return totalAmountOfEnemies - totalAmountOfEnemies % amountOfEnemiesInPack;
   //     }

   //     public int GetTotalAmountOfEnemies(float timeOfWave)
   //     {
   //         return (int)((timeOfWave - startSpawnTime - endSpawnTime) / spawnCd * (float)amountOfEnemiesInPack);
   //     }

   // }

}
