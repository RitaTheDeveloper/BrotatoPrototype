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

    public int WaveCounter { get => _waveCounter; }
    public GameObject[] PlayerPrefabs { get => playerPrefabs; }

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
        Destroy(GetComponent<SaveController>());
        SpawnPlayer(_heroIndex);
        _gameIsOver = false;
        _waveCounter = 0;
        _currentWave = _waves[_waveCounter];
        UIManager.instance.DisplayWaveNumber(_waveCounter + 1);
        Debug.Log("начинаем первую волну");
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
        SaveGameResult();
        Debug.Log("Game over!");
    }

    public void Win()
    {
        _gameIsOver = true;
        Debug.Log("Win!");
        _currentWave.StopWave();
        //StopTime();
        UIManager.instance.Win();
        SaveGameResult();
    }

    public void WaveCompleted()
    {
        LevelSystem playerLevelSystem = player.GetComponent<LevelSystem>();
        int numberOfleveledUpForCurrentWave = playerLevelSystem.NumberOfLeveledUpForCurrentWave;
       //StopTime();
        _waveCounter++;
        if (_waveCounter == _waves.Length)
        {
            Win();
        }
        else
        {
            player.GetComponent<PlayerSatiety>().ChangeSatiety(player.GetComponent<PlayerCharacteristics>().CurrentHunger);
            player.GetComponent<PlayerHealth>().Init();
            player.GetComponent<PlayerHealth>().DisplayHealth();
            UIManager.instance.WaveIsCompleted(numberOfleveledUpForCurrentWave);
            //UIManager.instance.WaveCompletedMenuOn(numberOfleveledUpForCurrentWave);
            playerLevelSystem.NumberOfLeveledUpForCurrentWave = 0;            
            RemoveAllEnemies();
            RemoveAllBullets();
            
        }              
    }

    public void StartNextWave()
    {
        player.GetComponent<PlayerHealth>().Init();
        player.GetComponent<PlayerHealth>().DisplayHealth();
        player.GetComponent<WeaponController>().EquipPlayer();
        player.GetComponent<PlayerSatiety>().ResetAmountOfFoodLifted();
        player.GetComponent<PlayerInventory>().ResetAmountOfWoodLifted();
        UIManager.instance.DisplayWaveNumber(_waveCounter + 1);        
        UIManager.instance.RemoveAllUpElements();
        RemoveAllCurrency();
        RemoveAllLoot();
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

    private void RemoveAllBullets()
    {
        Transform bullets = GameObject.Find("Bullets").transform;
        foreach(Transform bullet in bullets)
        {
            Destroy(bullet.gameObject);
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

    public void DestroyGameScene()
    {
        if (player != null)
        {
            Destroy(player);
        }

        RemoveAllEnemies();
        RemoveAllCurrency();
    }

    public void Restart()
    {
        if(player != null)
        {
            Destroy(player);
        }

        RemoveAllEnemies();
        RemoveAllBullets();
        RemoveAllCurrency();

        Init();
    }

    private void RemoveAllCurrency()
    {
        PoolObject.instance.RemoveAllObjectsFromScene();
    }

    private void RemoveAllLoot()
    {
        Transform lootContainer = GameObject.Find("Loot").transform;
        foreach (Transform loot in lootContainer)
        {
            Destroy(loot.gameObject);
        }
    }

    private void SaveGameResult()
    {
        SaveController save =  gameObject.AddComponent<SaveController>();
        save.LoadData();
        SaveData data = save.GetData();
        data.WaveEnded += _waveCounter;
        save.SetData(data);
        save.SaveData();
        Destroy(save);
    }

    public void LoadData()
    {
        gameObject.AddComponent<SaveController>();
    }
}
