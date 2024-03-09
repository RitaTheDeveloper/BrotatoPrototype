using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] public List<StandartItem> inventory = new List<StandartItem>();
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
        inventory.Add(item);
        PlayerCharacteristics playerCharacteristics = Player.GetComponent<PlayerCharacteristics>();
        if (Player && playerCharacteristics)
        {
            playerCharacteristics.AddBonus(item.CharacteristicsItem);
        }
    }
    
    public virtual void DeleteItem(StandartItem item)
    {
        inventory.Remove(item);
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
