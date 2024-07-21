using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponTemplateData : BaseTemplateData
{
    [Range(0.1f, 10f)]
    public float damageStrength;
    [Range(0.1f, 10f)]
    public float critChanceStrength;
    [Range(0.1f, 1000f)]
    public float damagePerSecond;
    [Range(0.1f, 10f)]
    public float additionalCharacteristicStrength;
}
