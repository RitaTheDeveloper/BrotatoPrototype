using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolObject : MonoBehaviour
{
    public static PoolObject instance;

    [SerializeField] private Currency _currencyPrefab;
    [SerializeField] private int maxPooolSize = 200;
    public ObjectPool<Currency> currencyPool;

    private void Awake()
    {
        instance = this;
        currencyPool = new ObjectPool<Currency>(CreateCurrency, OnTakeCurrencyFromPool, OnReturnCurrencyToPool, OnDestroyCurrency, true, 30, maxPooolSize);
    }

    private Currency CreateCurrency()
    {
        Currency currency = Instantiate(_currencyPrefab, transform);
        currency.SetPool(currencyPool);
        return currency;
    }

    private void OnTakeCurrencyFromPool(Currency currency)
    {
        //currency.transform.position = transform.position;
        //currency.transform.rotation = transform.rotation;

        currency.gameObject.SetActive(true);
    }

    private void OnReturnCurrencyToPool(Currency currency)
    {
        currency.gameObject.SetActive(false);
    }

    private void OnDestroyCurrency(Currency currency)
    {
        Destroy(currency.gameObject);
    }

    public void RemoveAllObjectsFromScene()
    {
        GameObject[] allCurrency = GameObject.FindGameObjectsWithTag("Currency");
        foreach(var currency in allCurrency)
        {
            currencyPool.Release(currency.GetComponent<Currency>());
        }
    }


}
