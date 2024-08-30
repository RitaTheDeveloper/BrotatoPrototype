using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterProgressMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _characterName;
    [SerializeField] private TextMeshProUGUI _characterLvl;
    [SerializeField] private Image _characterImage;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UICharacterProgressBar _uICharacterProgressBar;

    private CharacterLevelSetting[] _levelSettings;
    private GameObject _character;
    private SaveController _saveController;
    private UiPlayerInfo uiPlayerInfo;
    private int _currentNumberOfWaves;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _saveController = _gameManager.GetComponent<SaveController>();
        _levelSettings = _gameManager.CharacterLevelSystem.CharacterLevelSetting.levelSettings;
    }

    public void UpgradeUIParameters(GameObject character)
    {
        _character = character;
        uiPlayerInfo = _character.GetComponent<UiPlayerInfo>();
        NameUpdate();
        LevelUpdate();
        CurrentNumberOfWavesUpdate();
        SpriteUpdate();
        CreateProgressBar();
    }

    private void NameUpdate()
    {
        _characterName.text = uiPlayerInfo.nameHero;
    }

    private void LevelUpdate()
    {
        _characterLvl.text = _saveController.GetCharacterLvl(_character.gameObject.name).ToString();
    }

    private void SpriteUpdate()
    {
        _characterImage.sprite = uiPlayerInfo.player2d;
    }

    private void CreateProgressBar()
    {
        _uICharacterProgressBar.CreateProgressBar(_levelSettings, _currentNumberOfWaves);
    }

    private void CurrentNumberOfWavesUpdate()
    {
        _currentNumberOfWaves = _saveController.GetCharacterWaveCount(_character.gameObject.name);
    }
} 
