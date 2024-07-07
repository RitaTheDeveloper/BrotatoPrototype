using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitParameters : MonoBehaviour
{
    [SerializeField] private float _maxHp;
    [SerializeField] private float _startDamage;
    [SerializeField] private float _hpRegen;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _amountOfExperience;
    [SerializeField] private int _amountOfGoldForKill;
    [SerializeField] private GameObject _markOfSpawnPrefab;

    private float _currentHp;
    public float CurrentHp
    {
        get { return _currentHp; }
        set { _currentHp = value; }
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

    private float _currentDamage;
    public float CurrentDamage { get => _currentDamage; set => _currentDamage = value; }

    //public float AmountOfExperience {
    //    get
    //    {
    //        float finalExpAmount = _amountOfExperience * GameManager.instance.GetCurrentExpFactorForEnemy();

    //        if(finalExpAmount < 1f)
    //        {
    //            if (Random.Range(0,1f) < finalExpAmount)
    //            {
    //                finalExpAmount = 1f;
    //            }
    //            else
    //            {
    //                finalExpAmount = 0f;
    //            }
    //        }

    //        return finalExpAmount;
    //    }
    //}

    public int AmountOfGoldForKill { get => _amountOfGoldForKill; set => _amountOfGoldForKill = value; }
    public float AmountOfExperience { get => _amountOfExperience; set => _amountOfExperience = value; }

    public GameObject GetMark()
    {
        return _markOfSpawnPrefab;
    }

    private void Awake()
    {
        
    }

    public void Init(EnemyTierSettingStandart enemyTierSetting)
    {
        int indexOfWave = GameManager.instance.WaveCounter;
        //_currentHp = (_maxHp + indexOfWave * _amountOfHpPerWave) * GameManager.instance.GetCurrentHealthFactorForEnemy();
        //_currentDamage = (int)(_startDamage + indexOfWave * _amountOfDamagePerWave) * GameManager.instance.GetCurrentDamageFactorForEnemy();
        _currentHpRegen = _hpRegen;
        //_currentMoveSpeed = _moveSpeed;
        _currentHp = enemyTierSetting.HealPoint * GameManager.instance.listOfWaveSetting[indexOfWave].GetEnemyStrengthFactors.healthFactor;
        _currentDamage = enemyTierSetting.Damage * GameManager.instance.listOfWaveSetting[indexOfWave].GetEnemyStrengthFactors.damageFactor;
        _currentMoveSpeed = enemyTierSetting.Speed * GameManager.instance.listOfWaveSetting[indexOfWave].GetEnemyStrengthFactors.speedFactor;
    }
}
