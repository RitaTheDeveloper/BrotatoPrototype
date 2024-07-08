using HighlightPlus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Template", menuName = "Templates/Weapon Template")]
public class WeaponTemplate : BaseTemplate
{
    [Header("Template data for each weapon tier")]
    [SerializeField] private WeaponTemplateData[] weaponTemplateData;

    [SerializeField] private CritStrength critStrength;

    [SerializeField] private TierHighlightPairs tierHighlightPairs;

    public override BaseTemplateData GetTemplateDataForSpecificTier(TierType tier)
    {
        WeaponTemplateData dataToReturn = null;

        foreach (WeaponTemplateData specificData in weaponTemplateData)
        {
            if (specificData.tier == tier)
            {
                dataToReturn = specificData;
                break;
            }
        }

        if (dataToReturn == null)
        {
            throw new NullReferenceException();
        }

        return dataToReturn;
    }

    public HighlightProfile GetHighlightProfileForSprecificTier(TierType tier)
    {
        HighlightProfile profileToReturn = null;

        foreach (var specificData in tierHighlightPairs.highlightProfiles)
        {
            if (specificData.tier == tier)
            {
                profileToReturn = specificData.profile;
                break;
            }
        }

        if (profileToReturn == null)
        {
            throw new NullReferenceException();
        }

        return profileToReturn;
    }

    public float GetCritStrength() { return critStrength.value; }
}
