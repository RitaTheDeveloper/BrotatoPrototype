using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerStartingSpawnPoint;
    [SerializeField] private WaveController[] _waves;
    [SerializeField] WaveController _currentWave;

    public GameObject player;
    private int _waveCounter;
    private bool _gameIsOver;
    public bool GameIsOver { get { return _gameIsOver; } }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SpawnPlayer();
        _gameIsOver = false;
        _waveCounter = 0;
        _currentWave = _waves[_waveCounter];
        _waves[0].StartWave();
    }

    public void Lose()
    {
        _gameIsOver = true;
        _currentWave.StopWave();
        UIManager.instance.Lose();
        Debug.Log("Game over!");
    }

    public void Win()
    {
        Debug.Log("Win!");
        StopTime();
        UIManager.instance.Win();
    }

    public void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        StopTime();
        _waveCounter++;
        if (_waveCounter == _waves.Length)
        {
            Win();
        }
        else
        {
            UIManager.instance.AbilitySelectionPanelOn();
            RemoveAllEnemies();            
        }
              
    }

    public void StartNextWave()
    {
        player.GetComponent<PlayerHealth>().Init();
        player.GetComponent<PlayerHealth>().DisplayHealth();
        RemoveAllCurrency();
        ContinueTime();
        _currentWave = _waves[_waveCounter];
        _waves[_waveCounter].StartWave();
    }

    private void StopTime()
    {
        Time.timeScale = 0;
    }

    private void ContinueTime()
    {
        Time.timeScale = 1;
    }

    private void RemoveAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, playerStartingSpawnPoint.position, Quaternion.identity);
    }


    public void Restart()
    {
        if(player != null)
        {
            Destroy(player);
        }

        RemoveAllEnemies();
        RemoveAllCurrency();
        Init();
        ContinueTime();
    }

    private void RemoveAllCurrency()
    {
        Debug.Log("Удалить нахуй всю валюту с поля");
        PoolObject.instance.RemoveAllObjectsFromScene();
    }
}
