using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] public float time;
    [SerializeField] public List<EnemySpawner> enemySpawnersPrefabs;

    private List<WaveController2.EnemySpawnerSettings> enemySpawnerSettings;
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
    public void SetEnemySpawnerSettings(List<WaveController2.EnemySpawnerSettings> enemySpawnerSettings)
    {
        this.enemySpawnerSettings = enemySpawnerSettings;
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
        foreach(WaveController2.EnemySpawnerSettings enemySetting in enemySpawnerSettings)
        {
            var enemySpawnerObj = Instantiate(new GameObject(), transform);
            EnemySpawner enemySpawner = enemySpawnerObj.AddComponent<EnemySpawner>();
            enemySpawner.SetEnemy(enemySetting.enemy);
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