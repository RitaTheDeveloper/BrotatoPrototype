using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AccountLevel : MonoBehaviour, IUpgradable
{
    public ResultsOfRace resultsOfRace;

    private int _startLvl = 0;
    private int _currentLvl;
    private GameManager _gameManager;
    private SaveController _saveController;
    private AccountLevelSettingScriptable _accountLevelSettingScriptable;
    private AccountLevelSetting[] _accountLevelSettings;
    public int StartLvl => _startLvl;
    private List<GameObject> newlyUnlockedCharacterList = new List<GameObject>();

    public int CurrentLvl => _currentLvl;

    public AccountLevelSetting[] AccountLevelSettings { get => _accountLevelSettings; }

    public void Init(GameManager gameManager, AccountLevelSettingScriptable accountLevelSettingScriptable)
    {
        _gameManager = gameManager;
        _saveController = gameManager.GetComponent<SaveController>();
        _accountLevelSettingScriptable = accountLevelSettingScriptable;
        _accountLevelSettings = accountLevelSettingScriptable.accountLevelSettings;
        LoadCurrentLevel();
    }

    public void ResetProgress()
    {
        _currentLvl = _startLvl;
    }

    public void Upgrade()
    {
        if(GetSumOfLvlsOfOpenCharacters() >= _accountLevelSettingScriptable.accountLevelSettings[_currentLvl].numberOfCharacterLevels && CurrentLvl < _accountLevelSettingScriptable.accountLevelSettings.Length)
        {
            IncreaseLvl();
            resultsOfRace.AccountWasUpgraded(true, CurrentLvl);
        }
      
    }

    public int GetSumOfLvlsOfOpenCharacters()
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
        List<GameObject> list1 = GetUnlockCharacterList();
        _currentLvl++;
        SaveData data = _saveController.GetData();
        data.CurrentAccountLevel = _currentLvl;
        _saveController.SaveData();
        List<GameObject> list2 = GetUnlockCharacterList();
        newlyUnlockedCharacterList = list2.Except(list1).ToList();
       
    }

    private void LoadCurrentLevel()
    {
        _saveController.LoadData();
        _currentLvl = _saveController.GetData().CurrentAccountLevel;
    }

    public List<GameObject> GetUnlockCharacterList()
    {

        List<GameObject> result = new List<GameObject>();

        for (int i = 0; i < GameManager.instance.PlayerPrefabs.Length; i++)
        {
            if (GameManager.instance.PlayerPrefabs[i].GetComponent<UnlockCharacterComponent>().UnlockCharacter())
            {
                result.Add(GameManager.instance.PlayerPrefabs[i]);
            }
        }
        return result;
    }

    public List<GameObject> GetNewlyUnlockedCharacterList()
    {
        return newlyUnlockedCharacterList;
    }

    public void ResetNewlyUnlockedCharacterList()
    {
        newlyUnlockedCharacterList = new List<GameObject>();
    }

    public void ResultsOfGame()
    {

    }

}
