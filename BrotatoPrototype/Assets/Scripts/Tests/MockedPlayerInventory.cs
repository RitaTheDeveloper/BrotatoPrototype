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
        inventory.Add(item);
    }

    public override void DeleteItem(StandartItem item)
    {
        inventory.Remove(item);
    }
}
