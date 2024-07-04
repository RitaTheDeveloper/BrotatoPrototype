using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Template", menuName = "Templates/Weapon Template")]
public class WeaponTemplate : BaseTemplate
{
    [Header("Template data for each weapon tier")]
    [SerializeField] private WeaponTemplateData[] weaponTemplateData;

    [SerializeField] private CritStrength critStrength;

    public override BaseTemplateData GetTemplateDataForSpecificTier(TierType tier)
    {
        WeaponTemplateData dataToReturn = new WeaponTemplateData();

        foreach (WeaponTemplateData specificData in weaponTemplateData)
        {
            if (specificData.tier == tier)
            {
                dataToReturn = specificData;
                break;
            }
        }

        return dataToReturn;
    }

    public float GetCritStrength() { return critStrength.value; }
}
