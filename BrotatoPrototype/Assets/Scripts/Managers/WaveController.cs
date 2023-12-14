using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private List<GameObject> enemySpawners;

    private float _currentTime;
    private bool _stopTime;

    private void Start()
    {
        _stopTime = true;
        //_currentTime = time;
        //AllSpawnersOn();
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
        foreach(GameObject spawner in enemySpawners)
        {
            spawner.SetActive(true);
        }
    }

    private void AllSpawnersOff()
    {
        foreach (GameObject spawner in enemySpawners)
        {
            spawner.SetActive(false);
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