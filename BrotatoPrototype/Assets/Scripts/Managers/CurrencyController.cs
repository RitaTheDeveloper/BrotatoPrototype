using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    [SerializeField] private int startAmountOfCurrency = 0;
    private int _totalAmountOfCurrency;

    private void Start()
    {
        ResetTotalAmountOfCurrency();
        UIManager.instance.DisplayAmountOfCurrency(_totalAmountOfCurrency);
    }

    public void ChangeTotalAmountOfCurrency(int numberOfCurrency)
    {
        _totalAmountOfCurrency += numberOfCurrency;
        UIManager.instance.DisplayAmountOfCurrency(_totalAmountOfCurrency);
    }

    public int GetTotalAmountOfCurrency()
    {
        return _totalAmountOfCurrency;
    }

    private void ResetTotalAmountOfCurrency()
    {
        _totalAmountOfCurrency = startAmountOfCurrency;
    }
}
