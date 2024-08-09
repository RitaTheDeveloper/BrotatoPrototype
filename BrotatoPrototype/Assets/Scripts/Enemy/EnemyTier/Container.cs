using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Container")]
public class Container : ScriptableObject
{
    public List<EnemySetting> EnemiesSetting;
}
