using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Currency : MonoBehaviour
{
    private float xp;
    private ObjectPool<Currency> _pool;

    public void SetPool(ObjectPool<Currency> pool)
    {
        _pool = pool;
    }

    public void SetXP(float xp)
    {
        this.xp = xp;
    }

    public float GetXP()
    {
        return xp;
    }

    public void PutAwayFromScene()
    {
        _pool.Release(this);
    }
}
