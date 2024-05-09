using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int subdivisions = 10;
    public float radius;

    public void DrawTheCircle(float radius)
    {
        if (lineRenderer)
        {
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
            }

            float angleStep = 2f * Mathf.PI / subdivisions;
            lineRenderer.positionCount = subdivisions;

            for (int i = 0; i < subdivisions; i++)
            {
                float xPosition = radius * Mathf.Cos(angleStep * i);
                float zPosition = radius * Mathf.Sin(angleStep * i);

                Vector3 pointCircle = new Vector3(xPosition, 0f, zPosition);

                lineRenderer.SetPosition(i, pointCircle);
            }
        }
        
    }
}
