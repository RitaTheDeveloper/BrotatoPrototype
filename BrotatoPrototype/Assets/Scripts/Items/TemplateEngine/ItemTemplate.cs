using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Template", menuName = "Items Template/Template")]
public class ItemTemplate : ScriptableObject
{
    [Header("Tier templates")]
    [SerializeField] private DataPerTier[] data;

    [Space(20)]
    [Header("Base step for each characteristic")]
    [SerializeField] private BaseIncrementCharacteristics baseIncrement;

    [System.Serializable]
    public class DataPerTier
    {
        public RareItemsDataStruct tier;
        public float price;
        [Range(0.001f, 1000f)]
        public float baffStrength;
        [Range(0f, 1000f)]
        public float debaffStrength;
    }

    [System.Serializable]
    public class BaseIncrementCharacteristics
    {
        public float percentage = 0.005f;
        public float satiety = 0.05f;
        public float maxHealth = 0.5f;
        public float regenerationHP = 0.025f;
        public float dodge = 0.25f;
        public float armor = 0.25f;
        public float moveSpeed = 0.25f;
        public float attackSpeed = 0.5f;
        public float damage = 0.5f;
        public float meleeDamage = 0.2f;
        public float rangeDamage = 0.2f;
        public float chanceOfCrit = 0.1f;
        public float magneticRadius = 0.1f;
    }

    public DataPerTier GetTierTemplateData(RareItemsDataStruct tier)
    {
        DataPerTier dataToReturn = new DataPerTier();

        if (tier == null)
        {
            throw new ArgumentNullException("ItemTemplate::GetTierTemplateData() Parameter cannot be null");
        }

        foreach(DataPerTier datum in data)
        {
            if (datum.tier == tier)
            {
                dataToReturn = datum;
                break;
            }
        }

        return dataToReturn;
    }

    public BaseIncrementCharacteristics GetBaseIncrement()
    {
        return baseIncrement;
    }
}
