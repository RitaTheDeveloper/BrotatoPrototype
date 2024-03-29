using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private List<EnemySpawner> enemySpawnersPrefabs;

    private float _currentTime;
    private bool _stopTime;
    private bool _soundStart = false;

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

    private void CountingTime()
    {
        if (_currentTime > 0 && !_stopTime)
        {
            _currentTime -= Time.deltaTime;
            UIManager.instance.ShowTime(_currentTime);
            if (_currentTime <= 5 && !_soundStart)
            {
                _soundStart = true;
                PlaySoundEndWave();
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
        foreach(EnemySpawner spawner in enemySpawnersPrefabs)
        {
            var _enemySpawner = Instantiate(spawner);
            _enemySpawners.Add(_enemySpawner);
            _enemySpawner.transform.parent = transform;
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