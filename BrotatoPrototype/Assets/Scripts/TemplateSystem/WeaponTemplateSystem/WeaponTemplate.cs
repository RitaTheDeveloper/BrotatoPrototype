using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Template", menuName = "Templates/Weapon Template")]
public class WeaponTemplate : BaseTemplate
{
    [Header("Template data for each weapon tier")]
    [SerializeField] private WeaponTemplateData[] weaponTemplateData;
}
