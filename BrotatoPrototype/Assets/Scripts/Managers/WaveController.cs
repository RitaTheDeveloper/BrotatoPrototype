using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public float time;
    public List<EnemySpawner> enemySpawnersPrefabs;

    private List<WaveSetting.EnemySpawnerSettings> enemySpawnerSettings;
    private float _currentTime;
    private bool _stopTime;
    private bool _soundStart = false;
    float timerTick = 0;

    private List<EnemySpawner> _enemySpawners;
    DistrubitionOfGoldToMobs distrubitionOfGoldToMobs = new DistrubitionOfGoldToMobs();


    private void Start()
    {
        _stopTime = true;
        distrubitionOfGoldToMobs.GetNumberOfGold(100, 49);
    }

    private void FixedUpdate()
    {
        if (!_stopTime)
        {
            CountingTime();
        }
    }

    public void SetEnemySpawnerSettings(List<WaveSetting.EnemySpawnerSettings> enemySpawnerSettings)
    {
        this.enemySpawnerSettings = enemySpawnerSettings;
    }

    public float GetCurrentTime()
    {
        return _currentTime;
    }

    private void CountingTime()
    {
        if (_currentTime > 0 && !_stopTime)
        {
            _currentTime -= Time.deltaTime;
            UIManager.instance.ShowTime(_currentTime);
            if (_currentTime <= 5 && !_soundStart)
            {
                if (timerTick <= 0)
                {
                    PlaySoundEndWave();
                    timerTick = 1;
                }
                else
                    timerTick -= Time.deltaTime;
            }
        }
        else
        {
            StopWave();
            WaveCompleted();
        }
    }

    private void AllSpawnersOn()
    {
        //_enemySpawners = new List<EnemySpawner>();
        //foreach(EnemySpawner spawner in enemySpawnersPrefabs)
        //{
        //    var _enemySpawner = Instantiate(spawner);
        //    _enemySpawners.Add(_enemySpawner);
        //    _enemySpawner.transform.parent = transform;
        //}

        foreach (WaveSetting.EnemySpawnerSettings enemySetting in enemySpawnerSettings)
        {            
            float spawnCd = enemySetting.spawnCd;
            int totalAmountOfenemies = enemySetting.GetTotalAmountOfEnemies();
            if (spawnCd == -1 && totalAmountOfenemies == -1)
            {
                Debug.LogError("Необходимо выставить значение кд спавна или общее кол-во мобов за волну");
            }
            else if (totalAmountOfenemies != -1)
            {
                spawnCd = enemySetting.GetCdSpawn(time);
            }
            else
            {
                totalAmountOfenemies = enemySetting.GetTotalAmountOfEnemies(time);
                Debug.Log("totalAmount = " + totalAmountOfenemies);
            }

            Debug.Log("totalAmount = " + totalAmountOfenemies);
            var enemySpawnerObj = Instantiate(new GameObject("enemySpawner"), transform);
            enemySpawnerObj.transform.parent = transform.parent;
            EnemySpawner enemySpawner = enemySpawnerObj.AddComponent<EnemySpawner>();

            enemySpawner.SetParameters(enemySetting.enemy, spawnCd, enemySetting.startSpawnTime, enemySetting.endSpawnTime, enemySetting.amountOfEnemiesInPack);
            
        }
    }

    private void AllSpawnersOff()
    {
        foreach (Transform spawner in transform)
        {
            Destroy(spawner.gameObject);
        }
    }

    public void StartWave()
    {
        _stopTime = false;
        _currentTime = time;
        AllSpawnersOn();
    }

    public void StopWave()
    {
        _stopTime = true;
        AllSpawnersOff();
    }

    private void WaveCompleted()
    {
        GameManager.instance.WaveCompleted();
    }

    private void PlaySoundEndWave()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("TimerTick");
        }
    }

}

public class DistrubitionOfGoldToMobs
{
    public float averageNumberOfGoldForMob;
    public int firstNumberOfGold;
    public int secondNumberOfGold;
    public int firstNumberOfMobs;
    public int secondNumberOfMobs;

    public int GetNumberOfGold(int amountOfGoldPerWave, int amountOfMobsPerWave)
    {
        averageNumberOfGoldForMob = (float)amountOfGoldPerWave / (float)amountOfMobsPerWave;
        firstNumberOfGold = (int)Mathf.Ceil(averageNumberOfGoldForMob);
        secondNumberOfGold = Mathf.CeilToInt(averageNumberOfGoldForMob);
        Debug.Log("up = " + firstNumberOfGold + " down " + secondNumberOfGold);
        return 0; 
    }
}