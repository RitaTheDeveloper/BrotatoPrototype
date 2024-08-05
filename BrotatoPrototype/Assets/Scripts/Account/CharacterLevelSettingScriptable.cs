using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterLevelSettings", menuName = "Data/CharactertLevelSettings", order = 52)]
public class CharacterLevelSettingScriptable : ScriptableObject
{
    public CharacterLevelSetting[] levelSettings;
}

[System.Serializable]
public class CharacterLevelSetting
{
    public string name;
    public int numberOfWaves;
}
