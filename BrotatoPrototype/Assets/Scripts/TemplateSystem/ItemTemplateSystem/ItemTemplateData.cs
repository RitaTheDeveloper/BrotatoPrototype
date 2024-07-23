using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemTemplateData : BaseTemplateData
{
    [Range(0.001f, 1000f)]
    public float baffStrength;
    [Range(0f, 1000f)]
    public float debaffStrength;
}
