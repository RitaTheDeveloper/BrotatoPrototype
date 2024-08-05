using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountLevel : MonoBehaviour, IUpgradable
{
    private int _startLvl = 0;
    private int _currentLvl;
    private GameManager _gameManager;
    private SaveController _saveController;
    private AccountLevelSettingScriptable _accountLevelSettingScriptable;
    public int StartLvl => _startLvl;

    public int CurrentLvl => _currentLvl;

    public void Init(GameManager gameManager, AccountLevelSettingScriptable accountLevelSettingScriptable)
    {
        _gameManager = gameManager;
        _saveController = gameManager.GetComponent<SaveController>();
        _accountLevelSettingScriptable = accountLevelSettingScriptable;
        LoadCurrentLevel();
    }

    public void ResetProgress()
    {
        _currentLvl = _startLvl;
    }

    public void Upgrade()
    {
        if(GetSumOfLvlsOfOpenCharacters() >= _accountLevelSettingScriptable.accountLevelSettings[_currentLvl].numberOfCharacterLevels)
        {
            IncreaseLvl();
        }
      
    }

    private int GetSumOfLvlsOfOpenCharacters()
    {
        int sum = 0;
        for(int i=0; i < _gameManager.PlayerPrefabs.Length; i++)
        {
            sum += _saveController.GetCharacterLvl(_gameManager.PlayerPrefabs[i].name);
        }
        return sum;
    }

    private void IncreaseLvl()
    {
        _currentLvl++;
        SaveData data = _saveController.GetData();
        data.CurrentAccountLevel = _currentLvl;
        _saveController.SaveData();
    }

    private void LoadCurrentLevel()
    {
        _saveController.LoadData();
        _currentLvl = _saveController.GetData().CurrentAccountLevel;
    }
}
