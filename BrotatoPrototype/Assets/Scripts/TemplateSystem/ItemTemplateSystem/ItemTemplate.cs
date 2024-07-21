using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Template", menuName = "Templates/Item Template")]
public class ItemTemplate : BaseTemplate
{
    [Header("Tier templates")]
    [SerializeField] private ItemTemplateData[] itemTemplateData;

    [Header("Base step for each characteristic")]
    [SerializeField] private ItemCharacteristicIncrement baseIncrement;

    public override BaseTemplateData GetTemplateDataForSpecificTier(TierType tier)
    {
        ItemTemplateData dataToReturn = new ItemTemplateData();

        foreach (ItemTemplateData specificData in itemTemplateData)
        {
            if (specificData.tier == tier)
            {
                dataToReturn = specificData;
                break;
            }
        }

        return dataToReturn;
    }

    public ItemCharacteristicIncrement GetBaseIncrement()
    {
        return baseIncrement;
    }
}


