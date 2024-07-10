using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyStrengthFactors
{
    public float speedFactor;
    public float damageFactor;
    public float healthFactor;

    public EnemyStrengthFactors(float speedFactor, float damageFactor, float healthFactor)
        : this()
    {
        this.speedFactor = speedFactor;
        this.damageFactor = damageFactor;
        this.healthFactor = healthFactor;
    }
}

