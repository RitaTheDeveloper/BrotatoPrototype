using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArithmeticMethods
{
    // ���������� ������3 (�����), ������� ����� �� ���������� � �������� �����
    public static Vector3 PointOnTheCircle(Vector3 center, float radius, float angle)
    {        
        return center + Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward * radius;
    }
}
