using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacteristics : MonoBehaviour
{
    [SerializeField] private float _startMaxHp;
    [SerializeField] private float _startHpRegen;
    [SerializeField] private float _startMoveSpeed;
    [SerializeField] private float _startMagnetDistance;
    [SerializeField] private float _startAttackSpeedPercentage;
    [SerializeField] private float _startMelleeDamage;
    [SerializeField] private float _startRangedDamage;
    [SerializeField] private float _startCritChancePercentage;
    [SerializeField] private float _startArmor;
    [Range(0f, 100f)]
    [SerializeField] private float _startProbabilityOfDodge;
 
    private float _currentMaxHp;
    public float CurrentMaxHp
    {
        get { return _currentMaxHp; }
        set { _currentMaxHp = value; }
    }

    private float _currentHpRegen;
    public float CurrentHpRegen
    {
        get { return _currentHpRegen; }
        set { _currentHpRegen = value; }
    }

    private float _currentMoveSpeed;
    public float CurrentMoveSpeed
    {
        get { return _currentMoveSpeed; }
        set { _currentMoveSpeed = value; }
    }

    private float _currentMagnetDistance;
    public float CurrentMagnetDistance
    {
        get { return _currentMagnetDistance; }
        set { _currentMagnetDistance = value; }
    }

    private float _currentAttackSpeedPercentage;
    public float CurrentAttackSpeedPercentage
    {
        get { return _currentAttackSpeedPercentage; }
        set { _currentAttackSpeedPercentage = value; }
    }

    private float _currentMelleeDamage;
    public float CurrentMelleeDamage
    {
        get { return _currentMelleeDamage; }
        set { _currentMelleeDamage = value; }
    }

    private float _currentRangedDamage;
    public float CurrentRangedDamage { 
        get => _currentRangedDamage;
        set => _currentRangedDamage = value;
    }

    private float _currentCritChancePercentage;
    public float CurrentCritChancePercentage {
        get => _currentCritChancePercentage;
        set => _currentCritChancePercentage = value;
    }

    private float _currentArmor;
    public float CurrentArmor { get => _currentArmor; set => _currentArmor = value; }

    private float _currentProbabilityOfDodge;
    public float CurrentProbabilityOfDodge{ 
        get => _currentProbabilityOfDodge;
        set => _currentProbabilityOfDodge = value;
    }

    private void Awake()
    {
        _currentMoveSpeed = _startMoveSpeed;
        _currentMaxHp = _startMaxHp;
        _currentHpRegen = _startHpRegen;
        _currentMagnetDistance = _startMagnetDistance;
        _currentAttackSpeedPercentage = _startAttackSpeedPercentage;
        _currentMelleeDamage = _startMelleeDamage;
        _currentRangedDamage = _startRangedDamage;
        _currentCritChancePercentage = _startCritChancePercentage;
        _currentArmor = _startArmor;
        _currentProbabilityOfDodge = _startProbabilityOfDodge;
    }

    public void LevelUp()
    {
        _currentMaxHp += 1;
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        playerHealth.SetMaxHP();
        playerHealth.DisplayHealth();
    }

    public void AddBonus(PlayerCharacteristics bonus)
    {
        _currentMaxHp += bonus._currentMaxHp;
        _currentHpRegen += bonus._currentHpRegen;
        _currentMoveSpeed += bonus._currentMoveSpeed;
        _currentMagnetDistance += bonus._currentMagnetDistance;
        _currentAttackSpeedPercentage += bonus._currentAttackSpeedPercentage;
        _currentMelleeDamage += bonus._currentMelleeDamage;
        _currentRangedDamage += bonus._currentRangedDamage;
        _currentCritChancePercentage += bonus._currentCritChancePercentage;
    }

    public void DeleteBonus(PlayerCharacteristics bonus)
    {
        _currentMaxHp -= bonus._currentMaxHp;
        _currentHpRegen -= bonus._currentHpRegen;
        _currentMoveSpeed -= bonus._currentMoveSpeed;
        _currentMagnetDistance -= bonus._currentMagnetDistance;
        _currentAttackSpeedPercentage -= bonus._currentAttackSpeedPercentage;
        _currentMelleeDamage -= bonus._currentMelleeDamage;
        _currentRangedDamage -= bonus._currentRangedDamage;
        _currentCritChancePercentage -= bonus._currentCritChancePercentage;
    }
}
