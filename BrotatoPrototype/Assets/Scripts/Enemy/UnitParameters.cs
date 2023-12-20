using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitParameters : MonoBehaviour
{
    [SerializeField] private float _maxHp;
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

    public float AmountOfExperience { get { return _amountOfExperience; } }
}
