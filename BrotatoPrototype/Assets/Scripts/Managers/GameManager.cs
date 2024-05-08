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
    [SerializeField] private ShopController shop;

    public GameObject player;
    private int _waveCounter;
    private int _heroIndex = 0;
    private bool _gameIsOver;
    public bool GameIsOver { get { return _gameIsOver; } }

    private bool _isPlaying;
    public bool IsPlaying { get => _isPlaying; }
    public int WaveCounter { get => _waveCounter; }
    public GameObject[] PlayerPrefabs { get => playerPrefabs; }


    private void Awake()
    {
        instance = this;
        //ResetProgress();
    }
    private void Start()
    {
        _heroIndex = 0;
        //Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsPlaying)
        {
            PauseOn();
        }
    }

    public void Init()
    {
        Destroy(GetComponent<SaveController>());
        shop.ResetShop();
        SpawnPlayer(_heroIndex);
        _isPlaying = true;
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
        _isPlaying = false;
        _gameIsOver = true;
        _currentWave.StopWave();
        UIManager.instance.Lose();
        UIManager.instance.RemoveAllUpElements();
        RemoveAllCurrency();
        RemoveAllLoot();
        SaveGameResult();
        RemoveAllEnemies();
        RemoveAllBullets();
        shop.ResetShop();
        Debug.Log("Game over!");
    }

    public void Win()
    {
        _gameIsOver = true;
        Debug.Log("Win!");
        _currentWave.StopWave();
        //StopTime();
        UIManager.instance.Win();
        UIManager.instance.RemoveAllUpElements();
        RemoveAllCurrency();
        RemoveAllLoot();
        RemoveAllEnemies();
        RemoveAllBullets();
        shop.ResetShop();
        SaveGameResult();
    }

    public void WaveCompleted()
    {
        LevelSystem playerLevelSystem = player.GetComponent<LevelSystem>();
        int numberOfleveledUpForCurrentWave = playerLevelSystem.NumberOfLeveledUpForCurrentWave;
        _isPlaying = false;
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
        player.GetComponent<PlayerMovement>().PutPlayerInStartPosition(playerStartingSpawnPoint.position);
        _isPlaying = true;        
        player.GetComponent<PlayerHealth>().Init();
        player.GetComponent<PlayerHealth>().DisplayHealth();
        player.GetComponent<WeaponController>().EquipPlayer();
        player.GetComponent<PlayerSatiety>().ResetAmountOfFoodLifted();
        player.GetComponent<PlayerInventory>().ResetAmountOfWoodLiftedAndGoldForWave();
        UIManager.instance.DisplayWaveNumber(_waveCounter + 1);        
        UIManager.instance.RemoveAllUpElements();
        RemoveAllCurrency();
        RemoveAllLoot();
        _currentWave = _waves[_waveCounter];
        _waves[_waveCounter].StartWave();
    }

    private void PauseOn()
    {
        UIManager.instance.PauseMenu(true);
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        UIManager.instance.PauseMenu(false);
        Time.timeScale = 1;
    }

    private void RemoveAllEnemies()
    {
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform enemies = GameObject.Find("Enemies").transform;

        foreach (Transform enemy in enemies)
        {
            Destroy(enemy.gameObject);
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
        _isPlaying = false;
        _gameIsOver = true;
        _currentWave.StopWave();
        RemoveAllEnemies();
        if (player != null)
        {
            Destroy(player);
        }
        
        RemoveAllCurrency();
        RemoveAllLoot();
        UIManager.instance.RemoveAllUpElements();
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

        List<GameObject> unlockedCharacters = save.GetUnlockCharacterList(data);
        Debug.Log("unlocked characters: " + unlockedCharacters.Count);

        save.SaveData();
        Destroy(save);
    }

    public void LoadData()
    {
        gameObject.AddComponent<SaveController>();
    }

    private void ResetProgress()
    {
        SaveController save = gameObject.AddComponent<SaveController>();
        save.ResetData();
    }


}
