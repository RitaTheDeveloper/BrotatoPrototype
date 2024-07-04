using System;
using UnityEngine;

public class WeaponCreator : Creator
{
    public Weapon CreateWeapon(Weapon weapon, TierType tier)
    {
        Weapon weaponToReturn = Create(weapon, tier) as Weapon;

        if (weaponToReturn == null)
        {
            throw new NullReferenceException($" {weapon} : {tier} must be Weapon type");
        }

        return weaponToReturn;
    }
}
