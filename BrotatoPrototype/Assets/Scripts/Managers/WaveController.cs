using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private List<GameObject> enemySpawners;

    private float _currentTime;

    private void Start()
    {
        _currentTime = time;
        AllSpawnerOn();
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.GameIsOver)
        {
            CountingTime();
        }
    }

    private void CountingTime()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            UIManager.instance.ShowTime(_currentTime);
        }
    }

    private void AllSpawnerOn()
    {
        foreach(GameObject spawner in enemySpawners)
        {
            spawner.SetActive(true);
        }
    }

    public void AllSpawnerOff()
    {
        foreach (GameObject spawner in enemySpawners)
        {
            spawner.SetActive(false);
        }
    }
}