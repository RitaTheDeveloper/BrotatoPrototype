using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IUpgradable
{
    [SerializeField] private int _startLvl = 1;
    [SerializeField] private int _maxLvl = 5;
    [SerializeField] private Baff[] baffs;
    private int _currentLvl;

    public int StartLvl => _startLvl;

    public int CurrentLvl => _currentLvl;

    public int MaxLvl => _maxLvl;

    public void Init()
    {
        _currentLvl = _startLvl;
    }

    public void ResetProgress()
    {
        _currentLvl = _startLvl;
    }

    public void Upgrade()
    {
        throw new System.NotImplementedException();
    }
}
