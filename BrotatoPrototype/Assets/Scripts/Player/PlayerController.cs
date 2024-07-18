using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerHealth _health;
    private PlayerMovement _movement;
    private PlayerSatiety _satiety;
    private LevelSystem _levelSystem;
    private CharacterLevel _characterLevel;

    private void Awake()
    {
        _health = GetComponent<PlayerHealth>();
        _movement = GetComponent<PlayerMovement>();
        _levelSystem = GetComponent<LevelSystem>();
        _characterLevel = GetComponent<CharacterLevel>();
    }

    public void Init(GameManager gameManager, CharacterLevelSettingScriptable characterLevelSettings)
    {
        _characterLevel.Init(gameManager, characterLevelSettings);
    }

    public void UpdateCharacteristics()
    {
        _health.UpdateCharactestics();
        _movement.SetSpeed();
        _levelSystem.SetMagnetDistance();        
    }

}
