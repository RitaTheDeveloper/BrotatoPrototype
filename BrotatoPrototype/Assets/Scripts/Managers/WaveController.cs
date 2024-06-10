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
        _enemySpawners = new List<EnemySpawner>();
        //foreach(EnemySpawner spawner in enemySpawnersPrefabs)
        //{
        //    var _enemySpawner = Instantiate(spawner);
        //    _enemySpawners.Add(_enemySpawner);
        //    _enemySpawner.transform.parent = transform;
        //}

        //foreach(WaveSetting.EnemySpawnerSettings enemySetting in enemySpawnerSettings)
        //{
        //    float spawnCd = enemySetting.spawnCd;
        //    int totalAmountOfenemies = enemySetting.totalAmountOfEnemies;
        //    if (spawnCd == -1 && totalAmountOfenemies == -1)
        //    {
        //        Debug.LogError("Необходимо выставить значение кд спавна или общее кол-во мобов за волну");
        //    }
        //    else if (totalAmountOfenemies != -1)
        //    {
        //        spawnCd = enemySetting.GetCdSpawn(time);
        //    }
        //    else
        //    {
        //        totalAmountOfenemies = enemySetting.GetTotalAmountOfEnemies(time);
        //        Debug.Log("totalAmount = " + totalAmountOfenemies);
        //    }


        //    var enemySpawnerObj = Instantiate(new GameObject("enemySpawner"), transform);
        //    enemySpawnerObj.transform.parent = transform.parent;
        //    EnemySpawner enemySpawner = enemySpawnerObj.AddComponent<EnemySpawner>();

        //    enemySpawner.SetParameters(enemySetting.enemy, spawnCd, enemySetting.startSpawnTime, enemySetting.endSpawnTime, enemySetting.amountOfEnemiesInPack);
        //}

        for (int i = 0; i < enemySpawnerSettings.Count; i++)
        {
            float spawnCd = enemySpawnerSettings[i].spawnCd;
            int totalAmountOfenemies = enemySpawnerSettings[i].totalAmountOfEnemies;
            //int amountInPack =  totalAmountOfenemies - enemySpawnerSettings.Count;

            if (spawnCd == -1 && totalAmountOfenemies == -1)
            {
                Debug.LogError("Необходимо выставить значение кд спавна или общее кол-во мобов за волну");
            }
            else if (totalAmountOfenemies != -1)
            {
                spawnCd = enemySpawnerSettings[i].GetCdSpawn(time);
            }
            else
            {
                totalAmountOfenemies = enemySpawnerSettings[i].GetTotalAmountOfEnemies(time);
                Debug.Log("totalAmount = " + totalAmountOfenemies);
            }

            int x = enemySpawnerSettings[i].totalAmountOfEnemies % enemySpawnerSettings[i].amountOfEnemiesInPack;
            Debug.Log("x =" + x);
            if (x != 0)
            {
                var enemySpawnerObjDop = Instantiate(new GameObject("enemySpawner"), transform);
                enemySpawnerObjDop.transform.parent = transform.parent;
                EnemySpawner enemySpawnerDop = enemySpawnerObjDop.AddComponent<EnemySpawner>();
                enemySpawnerDop.SetParameters(enemySpawnerSettings[i].enemy, spawnCd * enemySpawnerSettings[i].totalAmountOfEnemies / enemySpawnerSettings[i].amountOfEnemiesInPack
                    , enemySpawnerSettings[i].startSpawnTime, enemySpawnerSettings[i].endSpawnTime, x);
                i++;
            }
            


            var enemySpawnerObj = Instantiate(new GameObject("enemySpawner"), transform);
            enemySpawnerObj.transform.parent = transform.parent;
            EnemySpawner enemySpawner = enemySpawnerObj.AddComponent<EnemySpawner>();

            enemySpawner.SetParameters(enemySpawnerSettings[i].enemy, spawnCd, enemySpawnerSettings[i].startSpawnTime, enemySpawnerSettings[i].endSpawnTime, enemySpawnerSettings[i].amountOfEnemiesInPack);
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