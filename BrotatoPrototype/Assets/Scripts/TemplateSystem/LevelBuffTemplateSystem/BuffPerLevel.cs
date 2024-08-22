using System;
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
        uIBuffPerLvl.dataTier = itemTemplate.GetPrefabDataForSpecificTier(tier);
        uIBuffPerLvl.tier = itemTemplate.GetPrefabDataForSpecificTier(tier).level;
        uIBuffPerLvl.icon = icon;
    }

    [ContextMenu("CalculateAllCharacteristics")]
    protected override void CalculateAllCharacteristics()
    {
        if (baffs.Length == 0)
        {
            throw new NotSupportedException($"{gameObject.name} baff list sould not be empty!");
        }

        ItemTemplateData data = itemTemplate.GetTemplateDataForSpecificTier(tier) as ItemTemplateData;
        ItemCharacteristicIncrement baseIncrement = itemTemplate.GetBaseIncrement();
       
        foreach (ItemBaff baff in baffs)
        {
            CalculateCharacteristic(baff.characteristic, baff.multiplier, baseIncrement, data.baffStrength, false);
        }

        if (debaffs.Length > 0)
        {
            foreach (ItemBaff debaff in debaffs)
            {
                CalculateCharacteristic(debaff.characteristic, debaff.multiplier, baseIncrement, data.debaffStrength, true);
            }
        }
        
    }
}
