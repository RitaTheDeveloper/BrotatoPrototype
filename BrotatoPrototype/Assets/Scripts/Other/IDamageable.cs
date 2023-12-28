
using UnityEngine;

public interface IDamageable
{
    void TakeHit(float damage, RaycastHit hit);

    void TakeHit(float damage);
}

