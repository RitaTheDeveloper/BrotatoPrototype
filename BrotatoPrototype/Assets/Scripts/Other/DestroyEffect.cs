using NTC.MonoCache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoCache
{
    private float _delay = 2;
    private float _timeOnDestroy;

    private void Awake()
    {
        _timeOnDestroy = Time.time + _delay;
    }

    protected override void FixedRun()
    {
        if (Time.time > _timeOnDestroy)
            Destroy(gameObject);
    }
}
