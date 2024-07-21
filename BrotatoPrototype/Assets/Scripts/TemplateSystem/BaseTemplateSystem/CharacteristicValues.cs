using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacteristicValues
{
    [ReadOnlyInspector] public float satiety;
    [ReadOnlyInspector] public float maxHealth;
    [ReadOnlyInspector] public float regenerationHP;
    [ReadOnlyInspector] public float dodge;
    [ReadOnlyInspector] public float armor;
    [ReadOnlyInspector] public float moveSpeed;
    [ReadOnlyInspector] public float attackSpeed;
    [ReadOnlyInspector] public float damage;
    [ReadOnlyInspector] public float meleeDamage;
    [ReadOnlyInspector] public float rangeDamage;
    [ReadOnlyInspector] public float critChance;
    [ReadOnlyInspector] public float magneticRadius;
}
