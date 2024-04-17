using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSatiety : MonoBehaviour
{
    private int _startSatiety = 100;
    private int _currentSatiety;
    private PlayerCharacteristics _playerCharacteristics;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        
    }

    private void Init()
    {
        _playerCharacteristics = GetComponent<PlayerCharacteristics>();
        _currentSatiety = _playerCharacteristics.CurrentSatiety;
        Debug.Log("satiety = " + _currentSatiety);
        UIManager.instance.DisplaySatiety(_currentSatiety, _startSatiety);        
    }

    public void ChangeSatiety(int hunger)
    {
        _currentSatiety += hunger;
        if (_currentSatiety < 0)
        {
            _currentSatiety = 0;
        }
        _playerCharacteristics.CurrentSatiety = _currentSatiety;
        UIManager.instance.DisplaySatiety(_currentSatiety, _startSatiety);
    }
}
