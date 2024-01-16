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

    public float AmountOfExperience { get { return _amountOfExperience; } }

    private void Awake()
    {
        _currentHp = _maxHp;
        _currentHpRegen = _hpRegen;
        _currentMoveSpeed = _moveSpeed;
        _currentDamage = _startDamage;
    }
}
