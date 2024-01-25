using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private List<EnemySpawner> enemySpawnersPrefabs;

    private float _currentTime;
    private bool _stopTime;

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
            Instantiate(spawner);
            _enemySpawners.Add(spawner);
        }
    }

    private void AllSpawnersOff()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (var spawner in spawners)
        {
            Destroy(spawner);
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
}