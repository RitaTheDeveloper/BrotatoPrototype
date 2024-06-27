using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] public List<StandartItem> inventory = new List<StandartItem>();
    [SerializeField] private int _startMoney = 0;
    [SerializeField] private int _startWood = 0;
    private int _currentMoney;
    private int _currentWood;
    private int _amountOfWoodLifted;
    private int _amountOfMoneyForWave;
    private int _amountOfWoodForWave;

    private PlayerCharacteristics playerCharacteristics;

    // Start is called before the first frame update
    void Start()
    {
        ResetAmountOfWoodLiftedAndGoldForWave();
        playerCharacteristics = GetComponent<PlayerCharacteristics>();
        ResetAllCurrencies();
        UIManager.instance.DisplayAmountOfCurrency(_currentMoney);
    }

    public void ResetAllCurrencies()
    {
        _currentMoney = _startMoney;
        _currentWood = _startWood;
        _amountOfMoneyForWave = 0;        
    }

    public virtual void AddItem(StandartItem item)
    {
        inventory.Add(item);
        if (playerCharacteristics)
        {
            playerCharacteristics.AddBonus(item.CharacteristicsItem);
            GetComponent<PlayerController>().UpdateCharacteristics();
            UIManager.instance.GetUIShop().UpdateUICharacteristics();
        }
    }
    
    public virtual void DeleteItem(StandartItem item)
    {
        inventory.Remove(item);
        if (playerCharacteristics)
        {
            playerCharacteristics.DeleteBonus(item.CharacteristicsItem);
            GetComponent<PlayerController>().UpdateCharacteristics();
            UIManager.instance.GetUIShop().UpdateUICharacteristics();
        }
    }

    public List<StandartItem> GetAllItems()
    {
        return inventory;
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

    public void MoneyUp(int money)
    {
        ChangeMoney(money);
        _amountOfMoneyForWave += money;
    }

    public void ChangeWood(int wood)
    {
        _currentWood += wood;
    }

    public int GetMoney()
    {
        return _currentMoney;
    }

    public int GetAmountOfMoneyForWave()
    {
        return _amountOfMoneyForWave;
    }

    public int GetWood()
    {
        return _currentWood;
    }

    public int GetAmountOfFoodLifted()
    {
        return _amountOfWoodLifted;
    }

    public int GetAmountOfWoodForWave()
    {
        return _amountOfWoodForWave;
    }

    public void WoodUp(int wood)
    {
        _amountOfWoodLifted++;
        _amountOfWoodForWave += wood;
        ChangeWood(wood);
        UIManager.instance.DisplayWoodUp(_amountOfWoodLifted);
    }

    public void ResetAmountOfWoodLiftedAndGoldForWave()
    {
        _amountOfWoodLifted = 0;
        _amountOfMoneyForWave = 0;
        _amountOfWoodForWave = 0;
    }
}
