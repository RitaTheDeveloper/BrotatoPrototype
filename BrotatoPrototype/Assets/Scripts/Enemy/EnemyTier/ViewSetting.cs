using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "View/ViewSetting")]
public class ViewSetting : ScriptableObject
{
    [Space(10)]
    public bool Highlighted;

    [Header("Outline")]
    [Range(0f, 1f)]
    public float Outline = 0.443f;
    public HighlightPlus.QualityLevel OutlineQuality = HighlightPlus.QualityLevel.Highest;
    public HighlightPlus.ContourStyle OutlineContourStyle = HighlightPlus.ContourStyle.AroundObjectShape;
    public float OutlineWidth = 0.2f;
    [Range(1, 3)]
    public int OutlineBlurPasses = 1;

    [Space(10)]
    [Header("Glow")]
    [Range(0, 5f)]
    public float Glow = 1;
    public HighlightPlus.QualityLevel glowQuality = HighlightPlus.QualityLevel.Highest;
    public float GlowWidth = 0.45f;
    public HighlightPlus.BlurMethod glowBlurMethod = HighlightPlus.BlurMethod.Kawase;
    [Range(1, 8)]
    public int GlowDownsampling = 1;
    [Space(10)]
    [Header("Color Tier")]
    public List<TierColor> TierColors;

    public TierColor GetColor(TierType tierType)
    {
        foreach(TierColor tierColor in TierColors)
            if (tierColor.tierType == tierType)
                return tierColor;

        return null;
    }
}

[System.Serializable]
public class TierColor
{
    public TierType tierType;
    public Color color1;
    public Color color2;
}