using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTemplate : ScriptableObject
{
    [Header("Tier-Prefab pairs")]
    public TierPrefabPairs tierPrefabPairs;

    public abstract BaseTemplateData GetTemplateDataForSpecificTier(TierType tier);

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
}
