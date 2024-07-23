using UnityEngine;

[System.Serializable]
public class ItemBaff : BaseBaff
{
    [Range(0.001f, 10f)]
    public float multiplier;
}
