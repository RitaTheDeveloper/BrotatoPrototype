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
    public DistrubitionOfGoldOrExpToMobs distrubitionOfGoldToMobs = new DistrubitionOfGoldOrExpToMobs();
    public DistrubitionOfGoldOrExpToMobs distrubitionOfExpToMobs = new DistrubitionOfGoldOrExpToMobs();
    private int _amountOfGoldForWave = 0;
    private int _amountOfExpForWave = 0;
    private int _amountOfEnemiesForAllSpawners = 0;
    public int counterOfMobs = 0;
    private GameObject mobSpawnerPrefab;

    public float CurrentTime { get => _currentTime; }

    private void Start()
    {
        _stopTime = true;
    }

    private void FixedUpdate()
    {
        if (!_stopTime)
        {
            CountingTime();
        }
    }

    public void SetWaveSettings(List<WaveSetting.EnemySpawnerSettings> enemySpawnerSettings, int goldForWave, int expForWave)
    {
        this.enemySpawnerSettings = enemySpawnerSettings;
        _amountOfGoldForWave = goldForWave;
        _amountOfExpForWave = expForWave;
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

    public int GetAmountOfTotalEnemiesPerWave()
    {
        return _amountOfEnemiesForAllSpawners;
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
        mobSpawnerPrefab = new GameObject("mobSpawnerPrefab");
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
            }

            Debug.Log("totalAmount = " + totalAmountOfenemies);
            _amountOfEnemiesForAllSpawners += totalAmountOfenemies;
            Debug.Log("totalAmountForAllSpawners = " + _amountOfEnemiesForAllSpawners);
            GameObject mobSpawner = Instantiate(mobSpawnerPrefab, transform);
            mobSpawner.transform.parent = transform;
            EnemySpawner enemySpawner = mobSpawner.AddComponent<EnemySpawner>();

            enemySpawner.SetParameters(enemySetting.enemy, spawnCd, enemySetting.startSpawnTime, enemySetting.endSpawnTime, enemySetting.amountOfEnemiesInPack);
        }
        Destroy(mobSpawnerPrefab);
    }

    private void AllSpawnersOff()
    {        
        foreach (Transform spawner in transform)
        {
            Destroy(spawner.gameObject);
        }
    }

    private void ResetWave()
    {
        _amountOfEnemiesForAllSpawners = 0;
        counterOfMobs = 0; // нужен только чтобы считать мобов дебаге, после тестов удалить
        distrubitionOfGoldToMobs.ResetCounter();
        AllSpawnersOff();
    }

    public void StartWave()
    {
        _stopTime = false;
        _currentTime = time;
        AllSpawnersOn();
        distrubitionOfGoldToMobs.CalculateParameters(_amountOfGoldForWave, _amountOfEnemiesForAllSpawners);
        distrubitionOfExpToMobs.CalculateParameters(_amountOfExpForWave, _amountOfEnemiesForAllSpawners);
       
    }

    public void StopWave()
    {
        _stopTime = true;
        ResetWave();
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

public class DistrubitionOfGoldOrExpToMobs
{
    public float averageNumberOfGoldForMob;
    public int firstNumberOfGold;
    public int secondNumberOfGold;
    public int firstNumberOfMobs;
    public int secondNumberOfMobs;
    private int _counterOfMobs = 0;

    public int GetNumberOfGoldOrExp()
    {
        _counterOfMobs++;
        int result = firstNumberOfGold;
        if(_counterOfMobs > firstNumberOfMobs) { result = secondNumberOfGold; }
        return result; 
    }

    public void ResetCounter()
    {
        _counterOfMobs = 0;
    }

    public void CalculateParameters(int amountOfGoldPerWave, int amountOfMobsPerWave)
    {
        averageNumberOfGoldForMob = (float)amountOfGoldPerWave / (float)amountOfMobsPerWave;
        firstNumberOfGold = Mathf.CeilToInt(averageNumberOfGoldForMob);
        secondNumberOfGold = (int)averageNumberOfGoldForMob;

        firstNumberOfMobs = amountOfGoldPerWave - amountOfMobsPerWave * secondNumberOfGold;
        secondNumberOfMobs = amountOfMobsPerWave - firstNumberOfMobs;
        Debug.Log("average = " + averageNumberOfGoldForMob + "; up = " + firstNumberOfGold + "; down " + secondNumberOfGold + "; 1 группа " + firstNumberOfMobs + "; 2 группа " + secondNumberOfMobs);
    }
}