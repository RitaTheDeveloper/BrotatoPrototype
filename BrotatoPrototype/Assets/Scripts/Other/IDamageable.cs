
using UnityEngine;

public interface IDamageable
{
    void TakeHit(float damage, bool isCrit, bool isProjectile);
}

