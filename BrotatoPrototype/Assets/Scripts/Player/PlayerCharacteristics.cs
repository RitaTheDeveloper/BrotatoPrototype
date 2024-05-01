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
    [Header("Скорость передвижения:")]
    [SerializeField] private float _startMoveSpeed;
    [Header("Радиус сбора:")]
    [SerializeField] private float _startMagnetDistance;
    [Header("% к скорости атаки:")]
    [SerializeField] private float _startAttackSpeedPercentage;
    [Header("% к урону:")]
    [SerializeField] private float _startDamagePercentage;
    [Header("+ к урону в ближнем бою:")]
    [SerializeField] private float _startMelleeDamage;
    [Header("+ к урону в дальнем бою:")]
    [SerializeField] private float _startRangedDamage;
    [Header("+ % к крит шансу:")]
    [SerializeField] private float _startCritChancePercentage;
    [Header("Броня:")]
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
    [SerializeField] private int _startHunger; // каждую волну на это число уменьшается сытость

    public Dictionary<string, float> DecriptionChacteristicsForUIShop()
    {
        Dictionary<string, float> descriptionForShop = new Dictionary<string, float>();
        descriptionForShop.Add(" к здоровью", _startMaxHp);
        descriptionForShop.Add(" к регенерации", _startHpRegen);
        descriptionForShop.Add("% к скорости", _startMoveSpeed);
        descriptionForShop.Add(" к радиусу сбора", _startMagnetDistance);
        descriptionForShop.Add("% к скорости атаки", _startAttackSpeedPercentage);
        descriptionForShop.Add("% к урону", _startDamagePercentage);
        descriptionForShop.Add(" к урону в ближнем бою", _startMelleeDamage);
        descriptionForShop.Add(" к урону в дальнем бою", _startRangedDamage);
        descriptionForShop.Add("% к крит шансу", _startCritChancePercentage);
        descriptionForShop.Add(" к броне", _startArmor);
        descriptionForShop.Add(" % к уклонению", _startProbabilityOfDodge);
        descriptionForShop.Add(" голод", _startHunger);

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
    public float CurrentDamagePercentage { get => _currentDamagePercentage; set => _currentDamagePercentage = value; }
   
    private float _currentDamagePercentage;

    private float _currentLifeStealPercentage;
    public float CurrentLifeStealPercentage { get => _currentLifeStealPercentage; set => _currentLifeStealPercentage = value; }

    private int _currentSatiety;
    public int CurrentSatiety { 
        get { return _currentSatiety; } 
        set { _currentSatiety = value; }
    }
    public int CurrentHunger { get => _currentHunger; set => _currentHunger = value; }

    private int _currentHunger;
    
   
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
        _currentMelleeDamage = _startMelleeDamage;
        _currentRangedDamage = _startRangedDamage;
        _currentCritChancePercentage = _startCritChancePercentage;
        _currentArmor = _startArmor;
        _currentProbabilityOfDodge = _startProbabilityOfDodge;
        _currentSatiety = _startSatiety;
        _currentHunger = _startHunger;
    }

    public void AddBonus(PlayerCharacteristics bonus)
    {
        _currentMaxHp += bonus._startMaxHp;
        _currentHpRegen += bonus._startHpRegen;
        _currentMoveSpeed += bonus._startMoveSpeed;
        _currentMagnetDistance += bonus._startMagnetDistance;
        _currentAttackSpeedPercentage += bonus._startAttackSpeedPercentage;
        _currentDamagePercentage += bonus._startDamagePercentage;
        _currentMelleeDamage += bonus._startMelleeDamage;
        _currentRangedDamage += bonus._startRangedDamage;
        _currentCritChancePercentage += bonus._startCritChancePercentage;
        _currentProbabilityOfDodge += bonus._startProbabilityOfDodge;
        _currentArmor += bonus._startArmor;
    }

    public void DeleteBonus(PlayerCharacteristics bonus)
    {
        _currentMaxHp -= bonus._startMaxHp;
        _currentHpRegen -= bonus._startHpRegen;
        _currentMoveSpeed -= bonus._startMoveSpeed;
        _currentMagnetDistance -= bonus._startMagnetDistance;
        _currentAttackSpeedPercentage -= bonus._startAttackSpeedPercentage;
        _currentMelleeDamage -= bonus._startMelleeDamage;
        _currentRangedDamage -= bonus._startRangedDamage;
        _currentCritChancePercentage -= bonus._startCritChancePercentage;
        _currentProbabilityOfDodge -= bonus._startProbabilityOfDodge;
        _currentArmor -= bonus._startArmor;
    }

    
}
