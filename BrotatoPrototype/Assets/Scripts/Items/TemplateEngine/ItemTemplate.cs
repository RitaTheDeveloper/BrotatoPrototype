using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Template", menuName = "Items Template/Template")]
public class ItemTemplate : ScriptableObject
{
    [Header("Tier templates")]
    [SerializeField] private TemplateData[] data;

    [Space(20)]
    [Header("Base step for each characteristic")]
    [SerializeField] private BaseCharacteristicsIncrement baseIncrement;

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
    public class BaseCharacteristicsIncrement
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

    public BaseCharacteristicsIncrement GetBaseIncrement()
    {
        return baseIncrement;
    }
}
