using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Template", menuName = "Templates/Item Template")]
public class ItemTemplate : BaseTemplate
{
    [Header("Tier templates")]
    [SerializeField] private ItemTemplateData[] itemTemplateData;

    [Header("Base step for each characteristic")]
    [SerializeField] private BaseCharacteristicIncrement baseIncrement;

    public ItemTemplateData GetTemplateDataForSpecificTier(TierType tier)
    {
        ItemTemplateData dataToReturn = new ItemTemplateData();

        foreach(ItemTemplateData specificData in itemTemplateData)
        {
            if (specificData.tier == tier)
            {
                dataToReturn = specificData;
                break;
            }
        }

        return dataToReturn;
    }

    public RareItemsDataStruct GetPrefabDataForSpecificTier(TierType tier)
    {
        RareItemsDataStruct dataFromPrefab = null;
        foreach (var pair in tierPrefabPairs.GetPairs())
        {
            if ((pair.tier == tier))
            {
                dataFromPrefab = pair.dataFromPefab;
                break;
            }
        }
        return dataFromPrefab;
    }

    public BaseCharacteristicIncrement GetBaseIncrement()
    {
        return baseIncrement;
    }
}


