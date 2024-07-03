using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tier Prefab Data", menuName = "Templates/Tier Prefab Pairs")]
public class TierPrefabPairs : ScriptableObject
{
    [SerializeField] private TierPrefabPair[] pairs;

    [System.Serializable]
    public class TierPrefabPair
    {
        public TierType tier;
        public RareItemsDataStruct dataFromPefab;
    }

    public TierPrefabPair[] GetPairs() { return pairs; }
}
