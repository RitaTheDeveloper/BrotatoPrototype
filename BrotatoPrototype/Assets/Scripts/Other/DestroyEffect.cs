using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    private float _delay = 2;
    private float _timeOnDestroy;

    private void Awake()
    {
        _timeOnDestroy = Time.time + _delay;
    }

    private void FixedUpdate()
    {
        if (Time.time > _timeOnDestroy)
            Destroy(gameObject);
    }
}
