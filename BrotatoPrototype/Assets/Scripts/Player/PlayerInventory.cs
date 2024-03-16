using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] public List<StandartItem> inventory = new List<StandartItem>();
    [SerializeField] public GameObject Player;
    [SerializeField] private int _startMoney = 0;
    [SerializeField] private int _startWood = 0;
    private int _currentMoney;
    private int _currentWood;

    private PlayerCharacteristics playerCharacteristics;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacteristics = GetComponent<PlayerCharacteristics>();
        ResetAllCurrencies();
        UIManager.instance.DisplayAmountOfCurrency(_currentMoney);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetAllCurrencies()
    {
        _currentMoney = _startMoney;
        _currentWood = _startWood;
    }

    public virtual void AddItem(StandartItem item)
    {
        inventory.Add(item);
        if (playerCharacteristics)
        {
            playerCharacteristics.AddBonus(item.CharacteristicsItem);
            UIShop.instance.UpdateUICharacteristics();
        }
    }
    
    public virtual void DeleteItem(StandartItem item)
    {
        inventory.Remove(item);
        if (playerCharacteristics)
        {
            playerCharacteristics.DeleteBonus(item.CharacteristicsItem);
        }
    }

    public bool HaveNeedCost(int cost)
    {
        if (_currentMoney - cost >= 0)
        {
            return true;
        }
        else
            return false;
    }

    public void ChangeMoney(int cost)
    {
        _currentMoney += cost;
        UIManager.instance.DisplayAmountOfCurrency(_currentMoney);
    }

    public bool HaveNeedWood(int wood)
    {
        if (_currentWood - wood >= 0)
        {
            return true;
        }
        else return false;
    }

    public void ChangeWood(int wood)
    {
        _currentWood += wood;
    }

    public int GetMoney()
    {
        return _currentMoney;
    }

    public int GetWood()
    {
        return _currentWood;
    }
}
