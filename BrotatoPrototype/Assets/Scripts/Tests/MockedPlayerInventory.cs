using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockedPlayerInventory : PlayerInventory
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void AddItem(StandartItem item)
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
    }

    public override void DeleteItem(StandartItem item)
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
    }
}
