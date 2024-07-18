using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterLevelSettings", menuName = "Data/CharactertLevelSettings", order = 52)]
public class CharacterLevelSettingScriptable : ScriptableObject
{
    public LevelSettings[] levelSettings;
}

[System.Serializable]
public class LevelSettings
{
    public string name;
    public int numberOfWaves;
}
