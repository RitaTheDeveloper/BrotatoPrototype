using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLevel : MonoBehaviour, IUpgradable
{
    public Action<int> onLevelUp;

    [SerializeField] private int _startLvl = 0;
    [SerializeField] private Baff[] baffs;
    private int _currentLvl;
    private int _maxLvl;
    private int _currentNumberOfWavesCompleted;
    private GameManager _gameManager;
    private CharacterLevelSettingScriptable _levelSettings;
    private SaveController _saveController;
    private PlayerCharacteristics _playerCharacteristics;
    private AccountLevel _accountLevel;
    public int StartLvl => _startLvl;

    public int CurrentLvl => _currentLvl;

    public int MaxLvl => _maxLvl;

    public Baff[] Baffs { get => baffs;}

    public void Init(GameManager gameManager, CharacterLevelSettingScriptable levelSettings)
    {
        _playerCharacteristics = GetComponent<PlayerCharacteristics>();        
        _gameManager = gameManager;
        _levelSettings = levelSettings;
        _saveController = _gameManager.GetComponent<SaveController>();
        _accountLevel = gameManager.AccountLevel;
        _gameManager.onWaveCompleted += Upgrade;
        LoadParameters();
    }

    private void OnDisable()
    {
        if(_gameManager) _gameManager.onWaveCompleted -= Upgrade;
    }

    public void ResetProgress()
    {
        _currentLvl = _startLvl;
        _currentNumberOfWavesCompleted = 0;
    }

    public void Upgrade()
    {
        _currentNumberOfWavesCompleted++;
        _saveController.SaveCharacterWaveCount(gameObject.name, _currentNumberOfWavesCompleted);

        if(_currentLvl < _maxLvl)
        {
            if (_currentNumberOfWavesCompleted >= _levelSettings.levelSettings[_currentLvl].numberOfWaves)
            {
                IncreaseLvl();
                UpgradeCharacteristics(_playerCharacteristics, 1);
            }
        }
        else
        {
            Debug.Log("level is max " + _currentLvl);
        }        
    }

    public void LoadParameters()
    {
        _currentNumberOfWavesCompleted = _saveController.GetCharacterWaveCount(gameObject.name);
        _currentLvl = _saveController.GetCharacterLvl(gameObject.name);
        _maxLvl = _levelSettings.levelSettings.Length;
        UpgradeCharacteristics(_playerCharacteristics, _saveController.GetCharacterLvl(gameObject.name));
    }

    private void IncreaseLvl()
    {
        _currentLvl++;
        _saveController.SaveCharacterLvl(gameObject.name, _currentLvl);
        _accountLevel.Upgrade();
        onLevelUp?.Invoke(_currentLvl);
        Debug.Log("level up " + _currentLvl);
    }

    public void UpgradeCharacteristics(PlayerCharacteristics playerCharacteristics, float level)
    {
        if (baffs != null)
        {
            Debug.Log("обновляем хар-ки");
            for (int i = 0; i < baffs.Length; i++)
            {
                playerCharacteristics.UpdateCurrentCharacteristic(baffs[i].characteristic, level * baffs[i].multiplier);
            }
        }
    }

}
