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

    private void Awake()
    {
        _currentMoveSpeed = _startMoveSpeed;
        _currentMaxHp = _startMaxHp;
        _currentHpRegen = _startHpRegen;
        _currentMagnetDistance = _startMagnetDistance;
        _currentAttackSpeedPercentage = _startAttackSpeedPercentage;
    }
}
