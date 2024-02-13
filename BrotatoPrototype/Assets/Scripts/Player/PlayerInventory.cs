using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] public Dictionary<string, StandartItem> inventory;
    [SerializeField] public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddItem(StandartItem item)
    {
        inventory.Add(item.KeyItem, item);
        PlayerCharacteristics playerCharacteristics = Player.GetComponent<PlayerCharacteristics>();
        if (Player && playerCharacteristics)
        {
            playerCharacteristics.AddBonus(item.CharacteristicsItem);
        }
    }
    
    void DeleteItem(StandartItem item)
    {
        inventory.Remove(item.KeyItem);
        PlayerCharacteristics playerCharacteristics = Player.GetComponent<PlayerCharacteristics>();
        if (Player && playerCharacteristics)
        {
            playerCharacteristics.DeleteBonus(item.CharacteristicsItem);
        }
    }
}
