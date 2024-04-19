using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGameObject: MonoBehaviour
{
    [SerializeField] private float speedRotate = 5f;

    private void Update()
    {
        transform.Rotate(0f, speedRotate * Time.deltaTime, 0f, Space.Self);
    }

}
