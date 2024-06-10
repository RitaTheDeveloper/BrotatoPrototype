
using UnityEngine;

public interface IDamageable
{
    void TakeHit(float damage, bool isCrit, bool isProjectile);

    void TakeHitDelayed(float damage, bool isCrit, bool isProjectile, float delay);
}

