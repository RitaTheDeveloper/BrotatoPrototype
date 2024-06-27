using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Template", menuName = "Items Template/Template")]
public class ItemTemplate : ScriptableObject
{
    [Header("Tier templates")]
    [SerializeField] private TemplateData[] data;

    [Space(20)]
    [Header("Base step for each characteristic")]
    [SerializeField] private BaseCharacteristicIncrement baseIncrement;
    
    [Space(40)]
    [SerializeField] private TierPrefabPair[] TierPrefabPairs;

    [System.Serializable]
    public class TemplateData
    {
        public TierType tier;
        public float price;
        [Range(0.001f, 1000f)]
        public float baffStrength;
        [Range(0f, 1000f)]
        public float debaffStrength;
    }

    [System.Serializable]
    public class TierPrefabPair
    {
        public TierType tier;
        public RareItemsDataStruct dataFromPefab;
    }


    public TemplateData GetTemplateDataForSpecificTier(TierType tier)
    {
        TemplateData dataToReturn = new TemplateData();

        foreach(TemplateData specificData in data)
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
        foreach (TierPrefabPair pair in TierPrefabPairs)
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


