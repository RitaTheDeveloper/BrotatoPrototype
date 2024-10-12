using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacteristics : MonoBehaviour
{
    [Header("Начальные параметры героя:")]
    [Space(20)]
    [Header("Здоровье:")]
    [SerializeField] private float _startMaxHp;
    [Header("Регенерация:")]
    [SerializeField] private float _startHpRegen;
    [Header("Скорость передвижения в %:")]
    [SerializeField] private float _startMoveSpeed;
    [Header("Радиус сбора:")]
    [SerializeField] private float _startMagnetDistance;
    [Header("% к скорости атаки:")]
    [SerializeField] private float _startAttackSpeedPercentage;
    [Header("% к урону:")]
    [SerializeField] private float _startDamagePercentage;
    [Header("+ к урону в ближнем бою:")]
    [SerializeField] private float _startMeleeDamage;
    [Header("+ к урону в дальнем бою:")]
    [SerializeField] private float _startRangedDamage;
    [Header("+ % к крит шансу:")]
    [SerializeField] private float _startCritChancePercentage;
    [Header("% брони:")]
    [SerializeField] private float _startArmor;
    [Header("Уклоние:")]
    [Range(0f, 100f)]
    [SerializeField] private float _startProbabilityOfDodge;
    //[Header("% к краже жизни:")]
    //[SerializeField] private float _startLifeStealPercentage;
    [Header("Сытость:")]
    [Range(0, 100)]
    [SerializeField] private int _startSatiety = 100;
    [Header("Голод:")]
    [SerializeField] private float _startHunger; // каждую волну на это число уменьшается сытость
    [Header("Премудрость:")]
    [SerializeField] private float _startWisdom;
    [SerializeField] private float _wisdomPerLvl = 5f;

    public Dictionary<string, float> DecriptionChacteristicsForUIShop()
    {
        Dictionary<string, float> descriptionForShop = new Dictionary<string, float>();
        descriptionForShop.Add(" макс. здоровье", _startMaxHp);
        descriptionForShop.Add(" к хп реген. в сек.", _startHpRegen);
        descriptionForShop.Add("% к скорости", _startMoveSpeed);
        descriptionForShop.Add(" к радиусу сбора", _startMagnetDistance);
        descriptionForShop.Add("% к скорости атаки", _startAttackSpeedPercentage);
        descriptionForShop.Add("% к урону", _startDamagePercentage);
        descriptionForShop.Add(" ближний бой", _startMeleeDamage);
        descriptionForShop.Add(" дальний бой", _startRangedDamage);
        descriptionForShop.Add("% к крит шансу", _startCritChancePercentage);
        descriptionForShop.Add("% к броне", _startArmor);
        descriptionForShop.Add("% к уклонению", _startProbabilityOfDodge);
        descriptionForShop.Add(" изменение сытости", _startHunger);

        return descriptionForShop;
    }

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

    private float _currentMeleeDamage;
    public float CurrentMeleeDamage
    {
        get { return _currentMeleeDamage; }
        set { _currentMeleeDamage = value; }
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
    public float CurrentDamagePercentage { get => _currentDamagePercentage; set => _currentDamagePercentage = value; }
   
    private float _currentDamagePercentage;

    private float _currentLifeStealPercentage;
    public float CurrentLifeStealPercentage { get => _currentLifeStealPercentage; set => _currentLifeStealPercentage = value; }

    private float _currentSatiety;
    public float CurrentSatiety { 
        get { return _currentSatiety; } 
        set { _currentSatiety = value; }
    }
    public float CurrentHunger { get => _currentHunger; set => _currentHunger = value; }
    public float CurrentWisdom { get => _currentWisdom; set => _currentWisdom = value; }

    private float _currentHunger;
    private float _currentWisdom;
    
   
    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _currentMoveSpeed = _startMoveSpeed;
        _currentMaxHp = _startMaxHp;
        _currentHpRegen = _startHpRegen;
        _currentMagnetDistance = _startMagnetDistance;
        _currentAttackSpeedPercentage = _startAttackSpeedPercentage;
        _currentDamagePercentage = _startDamagePercentage;
        _currentMeleeDamage = _startMeleeDamage;
        _currentRangedDamage = _startRangedDamage;
        _currentCritChancePercentage = _startCritChancePercentage;
        _currentArmor = _startArmor;
        _currentProbabilityOfDodge = _startProbabilityOfDodge;
        _currentSatiety = _startSatiety;
        _currentHunger = _startHunger;
        _currentWisdom = _startWisdom;
    }

    public void AddBonus(PlayerCharacteristics bonus)
    {
        _currentMaxHp += bonus._startMaxHp;
        _currentHpRegen += bonus._startHpRegen;
        _currentMoveSpeed += bonus._startMoveSpeed;
        _currentMagnetDistance += bonus._startMagnetDistance;
        _currentAttackSpeedPercentage += bonus._startAttackSpeedPercentage;
        _currentDamagePercentage += bonus._startDamagePercentage;
        _currentMeleeDamage += bonus._startMeleeDamage;
        _currentRangedDamage += bonus._startRangedDamage;
        _currentCritChancePercentage += bonus._startCritChancePercentage;
        _currentProbabilityOfDodge += bonus._startProbabilityOfDodge;
        _currentArmor += bonus._startArmor;
        _currentHunger += bonus._startHunger;
        _currentWisdom += bonus._startWisdom;
    }

    public void DeleteBonus(PlayerCharacteristics bonus)
    {
        _currentMaxHp -= bonus._startMaxHp;
        _currentHpRegen -= bonus._startHpRegen;
        _currentMoveSpeed -= bonus._startMoveSpeed;
        _currentMagnetDistance -= bonus._startMagnetDistance;
        _currentAttackSpeedPercentage -= bonus._startAttackSpeedPercentage;
        _currentDamagePercentage -= bonus._startDamagePercentage;
        _currentMeleeDamage -= bonus._startMeleeDamage;
        _currentRangedDamage -= bonus._startRangedDamage;
        _currentCritChancePercentage -= bonus._startCritChancePercentage;
        _currentProbabilityOfDodge -= bonus._startProbabilityOfDodge;
        _currentArmor -= bonus._startArmor;
        _currentHunger -= bonus._startHunger;
        _currentWisdom -= bonus._startWisdom;
    }
    
    public void IncreaseWisdomPerLevel()
    {
        _currentWisdom += _wisdomPerLvl;
    }
    
    public void SynchronizeCharacteristic(CharacteristicType characteristic, float value)
    {
        switch (characteristic)
        {
            case CharacteristicType.Satiety:
                _startHunger = value;
                break;

            case CharacteristicType.MaxHealth:
                _startMaxHp = value;
                break;

            case CharacteristicType.RegenerationHP:
                _startHpRegen = value;
                break;

            case CharacteristicType.Dodge:
                _startProbabilityOfDodge = value;
                break;

            case CharacteristicType.Armor:
                _startArmor = value;
                break;

            case CharacteristicType.MoveSpeed:
                _startMoveSpeed = value;
                break;

            case CharacteristicType.AttackSpeed:
                _startAttackSpeedPercentage = value;
                break;

            case CharacteristicType.Damage:
                _startDamagePercentage = value;
                break;

            case CharacteristicType.MeleeDamage:
                _startMeleeDamage = value;   
                break;

            case CharacteristicType.RangeDamage:
                _startRangedDamage = value;
                break;

            case CharacteristicType.ChanceOfCrit:
                _startCritChancePercentage = value;
                break;

            case CharacteristicType.MagneticRadius:
                _startMagnetDistance = value;
                break;
        }
    }
    public void UpdateCurrentCharacteristic(CharacteristicType characteristic, float value)
    {
        switch (characteristic)
        {
            case CharacteristicType.Satiety:
                CurrentHunger += Mathf.RoundToInt(value);
                break;

            case CharacteristicType.MaxHealth:
                CurrentMaxHp += value;
                break;

            case CharacteristicType.RegenerationHP:
                CurrentHpRegen += value;
                break;

            case CharacteristicType.Dodge:
                CurrentProbabilityOfDodge += value;
                break;

            case CharacteristicType.Armor:
                _currentArmor += value;
                break;

            case CharacteristicType.MoveSpeed:
                CurrentMoveSpeed += value;
                break;

            case CharacteristicType.AttackSpeed:
                CurrentAttackSpeedPercentage += value;
                break;

            case CharacteristicType.Damage:
                CurrentDamagePercentage += value;
                break;

            case CharacteristicType.MeleeDamage:
                CurrentMeleeDamage += value;
                break;

            case CharacteristicType.RangeDamage:
                CurrentRangedDamage += value;
                break;

            case CharacteristicType.ChanceOfCrit:
                CurrentCritChancePercentage += value;
                break;

            case CharacteristicType.MagneticRadius:
                CurrentMagnetDistance += value;
                break;

            case CharacteristicType.Wisdom:
                CurrentWisdom += value;
                break;
        }
    }
}
