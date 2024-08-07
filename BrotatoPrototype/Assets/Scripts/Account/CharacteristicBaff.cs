
using UnityEngine;

[System.Serializable]
public class CharacteristicBaff
{
    public CharacteristicType characteristic;
    [Range(0.001f, 50f)]
    public float value;
}
