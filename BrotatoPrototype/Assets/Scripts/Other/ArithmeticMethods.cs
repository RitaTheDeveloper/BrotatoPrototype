using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArithmeticMethods
{
    // возвращает векстор3 (точку), лежащем точно на окружности с заданным углом
    public static Vector3 PointOnTheCircle(Vector3 center, float radius, float angle)
    {
        float ang = angle;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}
