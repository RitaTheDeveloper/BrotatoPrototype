using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class GameManager : MonoBehaviour
{
    public Action onInit;
    public Action onGameOver;
    public Action onWaveCompleted;

    public static GameManager instance;

    [SerializeField] private bool resetProgress = false;
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private Transform playerStartingSpawnPoint;
    [SerializeField] private ShopController shop;
    [SerializeField] private ManagerOfWaves _managerOfWaves;
    [SerializeField] private PoolObject currencyPoolObject;
    [SerializeField] private AccountLevel _accountLevel;
    private int _currentDifficulty;
    public GameObject player;
    private List<WaveSetting> listOfWaveSetting;
    private int _waveCounter;
    private int _heroIndex = 0;
    private bool _gameIsOver;
    public bool GameIsOver { get { return _gameIsOver; } }

    private bool _isPlaying;
    public bool IsPlaying { get => _isPlaying; }
    public int WaveCounter { get => _waveCounter; }
    public GameObject[] PlayerPrefabs { get => playerPrefabs; }
    public int CurrentDifficulty { get => _currentDifficulty; set => _currentDifficulty = value; }
    public PoolObject GetCurrencyPoolObject { get => currencyPoolObject; }
    public List<WaveSetting> GetListOfWaveSetting { get => listOfWaveSetting; }
    public AccountLevel AccountLevel { get => _accountLevel; }

    public AudioMixerGroup MasterAudioMixer;
    public AudioMixerGroup MusicAudioMixer;
    public AudioMixerGroup SFXAudioMixer;
    private SaveController _saveController;
    private WaveController _currentWave;
    private CharacterLevelingSystem _characterLevelSystem;
    private AccountLevelingSystem _accountLevelingSystem;


    private void Awake()
    {
        instance = this;
        CurrentDifficulty = 0;
        _saveController = GetComponent<SaveController>();
        if (resetProgress)
        {
            ResetProgress();
        }
        listOfWaveSetting = _managerOfWaves.GetListOfWaveSettings();

        _characterLevelSystem = GetComponent<CharacterLevelingSystem>();
        _accountLevelingSystem = GetComponent<AccountLevelingSystem>();
        _accountLevel.Init(this, _accountLevelingSystem.AccountLevelSetting);
        //foreach (WaveSetting waveC2 in _listOfWaveSetting)
        //{
        //   // waveC2.CreateWave();
        //}
        _managerOfWaves.CreateWaves();        
    }
    private void Start()
    {
        _heroIndex = 0;
        InitSoundVolume();

    }

    private void OnEnable()
    {
        if (player)
        {
            player.GetComponent<PlayerHealth>().onPlayerDead += Lose;
        }
        
    }

    private void OnDisable()
    {
        if (player)
        {
            player.GetComponent<PlayerHealth>().onPlayerDead -= Lose;
        }

    }

    public void Init()
    {
        shop.ResetShop();
        SpawnPlayer(_heroIndex);
        player.GetComponent<PlayerHealth>().onPlayerDead += Lose;
        _isPlaying = true;
        _gameIsOver = false;
        
        _waveCounter = 0;
        // _currentWave = _waves[_waveCounter];
        _currentWave = listOfWaveSetting[_waveCounter].Wave;
        //_currentWave = wavesByDifficulty[CurrentDifficulty].listOfWaves[_waveCounter];
        //UIManager.instance.DisplayWaveNumber(_waveCounter + 1);
        _currentWave.StartWave();
        if (BackgroundMusicManger.instance != null)
        {
            BackgroundMusicManger.instance.ResetState();
            BackgroundMusicManger.instance.PlayBackgroundMusicFromMainMenuMusic();
        }

        onInit?.Invoke();
    }
    public int GetMaxWave()
    {
        return listOfWaveSetting.Count;
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
        RemoveAllFromArena();
        shop.ResetShop();
        onGameOver?.Invoke();
        UIManager.instance.DisplayUnLockedNewHeroes(GetListOfNewlyUnlockedCharacters());
        ResetListOfNewlyUnlockedCharacters();
    }

    public void Win()
    {
        _isPlaying = false;
        _gameIsOver = true;
        _currentWave.StopWave();
        UIManager.instance.Win();
        UIManager.instance.RemoveAllUpElements();
        RemoveAllFromArena();
        shop.ResetShop();
        UIManager.instance.DisplayUnLockedNewHeroes(GetListOfNewlyUnlockedCharacters());
        ResetListOfNewlyUnlockedCharacters();
    }

    public void WaveCompleted()
    {
        
        LevelSystem playerLevelSystem = player.GetComponent<LevelSystem>();
        int numberOfleveledUpForCurrentWave = playerLevelSystem.NumberOfLeveledUpForCurrentWave;
        _isPlaying = false;
       //StopTime();
        _waveCounter++;
        //if (_waveCounter == wavesByDifficulty[CurrentDifficulty].listOfWaves.Length)
        if (_waveCounter == listOfWaveSetting.Count)
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
        onWaveCompleted?.Invoke();
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
        //_currentWave = wavesByDifficulty[CurrentDifficulty].listOfWaves[_waveCounter];
        _currentWave = listOfWaveSetting[_waveCounter].Wave;
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
        CharacterLevelSettingScriptable characterLevelSetting = _characterLevelSystem.CharacterLevelSetting;
        player.GetComponent<PlayerController>().Init(this, characterLevelSetting);
    }

    public void DestroyGameScene()
    {
        _isPlaying = false;
        _gameIsOver = true;
        _currentWave.StopWave();

        if (player != null)
        {
            Destroy(player);
        }
        RemoveAllFromArena();        
    }

    public void Restart()
    {
        if(player != null)
        {
            Destroy(player);
        }

        RemoveAllFromArena();
        Init();
    }

    private void RemoveAllCurrency()
    {
        currencyPoolObject.RemoveAllObjectsFromScene();
    }

    private void RemoveAllLoot()
    {
        Transform lootContainer = GameObject.Find("Loot").transform;
        foreach (Transform loot in lootContainer)
        {
            Destroy(loot.gameObject);
        }
    }

    private void RemoveAllFromArena()
    {
        RemoveAllBullets();
        RemoveAllCurrency();
        RemoveAllEnemies();
        RemoveAllLoot();
    }

    private List<GameObject> GetListOfNewlyUnlockedCharacters()
    {

        return AccountLevel.GetNewlyUnlockedCharacterList();
    }

    private void ResetListOfNewlyUnlockedCharacters()
    {
        AccountLevel.ResetNewlyUnlockedCharacterList();
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
    
    //public float GetCurrentExpFactorForEnemy()
    //{
    //    return wavesByDifficulty[CurrentDifficulty].expFactor;
    //}

    //public float GetCurrentHealthFactorForEnemy()
    //{
    //    return wavesByDifficulty[CurrentDifficulty].healthFactor;
    //}
    //public float GetCurrentDamageFactorForEnemy()
    //{
    //    return wavesByDifficulty[CurrentDifficulty].damageFactor;
    //}

    public WaveController GetCurrentWave()
    {
        return _currentWave;
    }
}

[System.Serializable]
public struct DifficultyOfWaves
{
    public float expFactor;
    public float healthFactor;
    public float damageFactor;
    public WaveController[] listOfWaves;

    public DifficultyOfWaves(WaveController[] listOfWaves)
        :this()
    {
        expFactor = 1;
        healthFactor = 1;
        damageFactor = 1;
        this.listOfWaves = listOfWaves;
    }
}
