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

    public void InitParametrs(EnemyTierSettingStandart enemyTierSetting)
    {
        _maxHp = enemyTierSetting.HealPoint;
        _startDamage = enemyTierSetting.Damage;
        _moveSpeed = enemyTierSetting.Speed;

        int indexOfWave = GameManager.instance.WaveCounter;

        _currentHpRegen = _hpRegen;
        _currentHp = _maxHp * GameManager.instance.GetListOfWaveSetting[indexOfWave].GetEnemyStrengthFactors.healthFactor;
        _currentDamage = _startDamage * GameManager.instance.GetListOfWaveSetting[indexOfWave].GetEnemyStrengthFactors.damageFactor;
        _currentMoveSpeed = _moveSpeed * GameManager.instance.GetListOfWaveSetting[indexOfWave].GetEnemyStrengthFactors.speedFactor;
    }
}
