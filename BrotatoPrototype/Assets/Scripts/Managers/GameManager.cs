using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private Transform playerStartingSpawnPoint;
    [SerializeField] private WaveController[] _waves;
    [SerializeField] WaveController _currentWave;

    public GameObject player;
    private int _waveCounter;
    private int _heroIndex = 0;
    private bool _gameIsOver;
    public bool GameIsOver { get { return _gameIsOver; } }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _heroIndex = 0;
        //Init();
    }

    public void Init()
    {
        SpawnPlayer(_heroIndex);
        _gameIsOver = false;
        _waveCounter = 0;
        _currentWave = _waves[_waveCounter];
        _waves[0].StartWave();
    }

    public void SetHeroIndex(int index)
    {
        _heroIndex = index;
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

    private void SpawnPlayer(int index)
    {
        if (player != null)
        {
            Destroy(player);            
        }

        player = Instantiate(playerPrefabs[index], playerStartingSpawnPoint.position, Quaternion.identity);
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
        PoolObject.instance.RemoveAllObjectsFromScene();
    }
}
