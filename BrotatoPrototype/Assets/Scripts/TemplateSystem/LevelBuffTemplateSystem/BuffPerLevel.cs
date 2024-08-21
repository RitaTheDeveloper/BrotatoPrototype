using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPerLevel : Item
{
    public override void SynchronizeComponents()
    {
        SynchronizePlayerCharacteristics();
        SynchronizeItemShopInfo();
    }

    protected override void SynchronizeItemShopInfo()
    {
        UIBuffPerLvl uIBuffPerLvl = GetComponent<UIBuffPerLvl>();
        uIBuffPerLvl.mainCharacteristic = baffs[0].characteristic;
        uIBuffPerLvl.value = characteristicMap[baffs[0].characteristic];
        uIBuffPerLvl.tier = itemTemplate.GetPrefabDataForSpecificTier(tier).level;
    }
}
