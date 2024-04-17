using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSatiety : MonoBehaviour
{
    private int _startSatiety = 100;
    private int _currentSatiety;
    private PlayerCharacteristics _playerCharacteristics;
    private int _amountOfFoodLifted;
    private bool isFull = false;
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        
    }

    private void Init()
    {
        ResetAmountOfFoodLifted();
        _playerCharacteristics = GetComponent<PlayerCharacteristics>();
        _currentSatiety = _playerCharacteristics.CurrentSatiety;
        Debug.Log("satiety = " + _currentSatiety);
        UIManager.instance.DisplaySatiety(_currentSatiety, _startSatiety, isFull);        
    }

    public void ChangeSatiety(int hunger)
    {
        _currentSatiety += hunger;
        if (_currentSatiety < 0)
        {
            _currentSatiety = 0;
        }
        if (_currentSatiety >= 100)
        {
            isFull = true;
        }
        else isFull = false;
        _playerCharacteristics.CurrentSatiety = _currentSatiety;
        UIManager.instance.DisplaySatiety(_currentSatiety, _startSatiety, isFull);
    }

    public int GetAmountOfFoodLifted()
    {
        return _amountOfFoodLifted;
    }

    public void FoodUp()
    {
        _amountOfFoodLifted++;
        UIManager.instance.DisplayFoodUp(_amountOfFoodLifted);
    }

    public void ResetAmountOfFoodLifted()
    {
        _amountOfFoodLifted = 0;
    }
}
