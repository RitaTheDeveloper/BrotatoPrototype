using NTC.MonoCache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGameObject: MonoCache
{
    [SerializeField] private float speedRotate = 5f;

    protected override void Run()
    {
        transform.Rotate(0f, speedRotate * Time.deltaTime, 0f, Space.Self);
    }

}
