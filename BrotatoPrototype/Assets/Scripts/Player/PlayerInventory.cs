using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] public Dictionary<string, StandartItem> inventory = new Dictionary<string, StandartItem>();
    [SerializeField] public Dictionary<string, int> countItems = new Dictionary<string, int>();
    [SerializeField] public GameObject Player;
    public int MoneyPlayer = 0;
    public int WoodPlayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void AddItem(StandartItem item)
    {
        if (inventory.ContainsKey(item.IdItem))
        {
            countItems[item.IdItem]++;
        }
        else
        {
            inventory.Add(item.IdItem, item);
            countItems[item.IdItem] = 1;
        }
        PlayerCharacteristics playerCharacteristics = Player.GetComponent<PlayerCharacteristics>();
        if (Player && playerCharacteristics)
        {
            playerCharacteristics.AddBonus(item.CharacteristicsItem);
        }
    }
    
    public virtual void DeleteItem(StandartItem item)
    {
        inventory.Remove(item.IdItem);
        if (countItems.ContainsKey(item.IdItem) && countItems[item.IdItem] > 1)
        {
            countItems[item.IdItem]--;
        }
        else if (countItems.ContainsKey(item.IdItem) && countItems[item.IdItem] == 1)
        {
            inventory.Remove(item.IdItem);
            countItems.Remove(item.IdItem);
        }
        PlayerCharacteristics playerCharacteristics = Player.GetComponent<PlayerCharacteristics>();
        if (Player && playerCharacteristics)
        {
            playerCharacteristics.DeleteBonus(item.CharacteristicsItem);
        }
    }
    public bool HaveNeedCost(int cost)
    {
        if (MoneyPlayer - cost >= 0)
        {
            return true;
        }
        else
            return false;
    }
    public void ChangeMoney(int cost)
    {
        MoneyPlayer += cost;
    }
    public bool HaveNeedWood(int wood)
    {
        if (WoodPlayer - wood >= 0)
        {
            return true;
        }
        else return false;
    }
    public void ChangeWood(int wood)
    {
        WoodPlayer += wood;
    }
}
