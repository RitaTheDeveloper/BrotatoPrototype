using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponModifiers", menuName = "Data/Weapon Modifiers")]
public class WeaponModifiers : ScriptableObject
{
    public Modifiers[] modifiers = new Modifiers[4];

    [System.Serializable]
    public struct Modifiers
    {
        public float forDamage;
        public float forAttackSpeed;
        public float forCritChance;
    }
}


