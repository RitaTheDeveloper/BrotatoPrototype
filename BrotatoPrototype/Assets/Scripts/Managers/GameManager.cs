using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class GameManager : MonoBehaviour
{
    public Action onInit;

    public static GameManager instance;

    [SerializeField] private bool resetProgress = false;
    [SerializeField] private AnalyticsSystem analyticsSystem;
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private Transform playerStartingSpawnPoint;
    [SerializeField] private DifficultyOfWaves[] wavesByDifficulty;
    [SerializeField] WaveController _currentWave;
    [SerializeField] private ShopController shop;

    private int _currentDifficulty;
    public GameObject player;
    private int _waveCounter;
    private int _heroIndex = 0;
    private bool _gameIsOver;
    public bool GameIsOver { get { return _gameIsOver; } }

    private bool _isPlaying;
    public bool IsPlaying { get => _isPlaying; }
    public int WaveCounter { get => _waveCounter; }
    public GameObject[] PlayerPrefabs { get => playerPrefabs; }
    public int CurrentDifficulty { get => _currentDifficulty; set => _currentDifficulty = value; }

    public AudioMixerGroup MasterAudioMixer;
    public AudioMixerGroup MusicAudioMixer;
    public AudioMixerGroup SFXAudioMixer;
    private SaveController _saveController;


    private void Awake()
    {
        instance = this;
        CurrentDifficulty = 0;
        _saveController = GetComponent<SaveController>();
        if (resetProgress)
        {
            ResetProgress();
        }        
    }
    private void Start()
    {
        _heroIndex = 0;
        InitSoundVolume();
        //Init();
    }

    public void Init()
    {
        shop.ResetShop();
        SpawnPlayer(_heroIndex);
        analyticsSystem.OnStartedPlaying(player.name);
        Debug.Log("startedPlaying " + player.name);
        _isPlaying = true;
        _gameIsOver = false;
        _waveCounter = 0;
        // _currentWave = _waves[_waveCounter];
        _currentWave = wavesByDifficulty[CurrentDifficulty].listOfWaves[_waveCounter];
        UIManager.instance.DisplayWaveNumber(_waveCounter + 1);
        _currentWave.StartWave();
        if (BackgroundMusicManger.instance != null)
        {
            BackgroundMusicManger.instance.ResetState();
            BackgroundMusicManger.instance.PlayBackgroundMusicFromMainMenuMusic();
        }

        onInit?.Invoke();
    }
    
    public void SetHeroIndex(int index)
    {
        _heroIndex = index;
    }

    public void Lose()
    {
        analyticsSystem.OnGameOver();
        _isPlaying = false;
        _gameIsOver = true;
        _currentWave.StopWave();
        analyticsSystem.OnPlayerDead(_waveCounter, player.name);
        UIManager.instance.Lose();
        UIManager.instance.RemoveAllUpElements();
        RemoveAllCurrency();
        RemoveAllLoot();
        SaveGameResult();
        RemoveAllEnemies();
        RemoveAllBullets();
        shop.ResetShop();
    }

    public void Win()
    {
        _gameIsOver = true;
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
        analyticsSystem.WaveCompleted(_waveCounter, player.name);
        if (_waveCounter == wavesByDifficulty[CurrentDifficulty].listOfWaves.Length)
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
        //_currentWave = _waves[_waveCounter];
        _currentWave = wavesByDifficulty[CurrentDifficulty].listOfWaves[_waveCounter];
        _currentWave.StartWave();
    }

    private void PauseOn()
    {
        UIManager.instance.PauseMenu(true);
        Time.timeScale = 0;
    }

    public void PauseOnTriggered()
    {
        if (IsPlaying)
        {
            PauseOn();
        }
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
        _saveController.LoadData();
        SaveData data = new SaveData();
        data.WaveEnded = _saveController.GetData().WaveEnded;
        data.WaveEnded += _waveCounter;
        data.SFXVolume = _saveController.GetData().SFXVolume;
        data.MusicSondVolume = _saveController.GetData().MusicSondVolume;
        data.MasterSoundVolume = _saveController.GetData().MasterSoundVolume;

        List<GameObject> unlockedCharacters = _saveController.GetUnlockCharacterList(data);
        UIManager.instance.DisplayUnLockedNewHeroes(unlockedCharacters);
        Debug.Log("unlocked characters: " + unlockedCharacters.Count);

        _saveController.SaveData();
    }

    public void LoadData()
    {
        _saveController.LoadData();
    }

    private void ResetProgress()
    {
        _saveController.ResetData();
    }

    private void InitSoundVolume()
    {
        _saveController.LoadData();
        SaveData saveData = _saveController.GetData();

        MasterAudioMixer.audioMixer.SetFloat("MasterVulomeParam", saveData.MasterSoundVolume);
        MusicAudioMixer.audioMixer.SetFloat("BackGroundMusicVolumeParam", saveData.MusicSondVolume);
        SFXAudioMixer.audioMixer.SetFloat("SFXVolumeParam", saveData.SFXVolume);

    }  
    
    public float GetCurrentExpFactorForEnemy()
    {
        return wavesByDifficulty[CurrentDifficulty].expFactor;
    }

    public float GetCurrentHealthFactorForEnemy()
    {
        return wavesByDifficulty[CurrentDifficulty].healthFactor;
    }
    public float GetCurrentDamageFactorForEnemy()
    {
        return wavesByDifficulty[CurrentDifficulty].damageFactor;
    }
}

[System.Serializable]
public struct DifficultyOfWaves
{
    public float expFactor;
    public float healthFactor;
    public float damageFactor;
    public WaveController[] listOfWaves;
}
