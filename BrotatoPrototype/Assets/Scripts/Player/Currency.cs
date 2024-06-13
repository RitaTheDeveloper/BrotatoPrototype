using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Currency : MonoBehaviour
{
    private int _gold;
    private float _xp;
    private ObjectPool<Currency> _pool;

    public int Gold { get => _gold; set => _gold = value; }

    public void SetPool(ObjectPool<Currency> pool)
    {
        _pool = pool;
    }

    public void SetXP(float xp)
    {
        this._xp = xp;
    }

    public float GetXP()
    {
        return _xp;
    }

    public void PutAwayFromScene()
    {
        _pool.Release(this);
    }
}
