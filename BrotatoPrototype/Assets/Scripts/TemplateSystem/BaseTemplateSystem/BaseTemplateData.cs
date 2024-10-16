using UnityEngine;

[System.Serializable]
public abstract class BaseTemplateData 
{
    public string name;
    public TierType tier;
    [Range(1f, 1000f)]
    public float price;
}
