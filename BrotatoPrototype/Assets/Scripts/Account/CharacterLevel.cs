using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLevel : MonoBehaviour, IUpgradable
{
    [SerializeField] private int _startLvl = 0;
    [SerializeField] private Baff[] baffs;
    private int _currentLvl;
    private int _maxLvl;
    private int _currentNumberOfWavesPassed;
    private GameManager _gameManager;
    private CharacterLevelSettingScriptable _levelSettings;

    public int StartLvl => _startLvl;

    public int CurrentLvl => _currentLvl;

    public int MaxLvl => _maxLvl;

    public void Init(GameManager gameManager, CharacterLevelSettingScriptable levelSettings)
    {
        _currentLvl = _startLvl;
        _gameManager = gameManager;
        _levelSettings = levelSettings;
    }

    public void ResetProgress()
    {
        _currentLvl = _startLvl;
        _currentNumberOfWavesPassed = 0;
    }

    public void Upgrade()
    {
        _currentNumberOfWavesPassed++;
        if (_currentNumberOfWavesPassed >= _levelSettings.levelSettings[_currentLvl].numberOfWaves)
        {
            _currentLvl++;
            Debug.Log("level up " + _currentLvl);
        }
    }

}
