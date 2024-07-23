using UnityEngine;

[CreateAssetMenu(fileName = "New Crit Strength", menuName = "Templates/Crit Strength")]
[System.Serializable]
public class CritStrength : ScriptableObject
{
    [Min(1.0001f)]
    public float value = 1.0001f;
}
