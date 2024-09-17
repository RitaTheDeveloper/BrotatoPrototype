using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AccountLevelSettings", menuName = "Data/AccountLevelSettings", order = 51)]
public class AccountLevelSettingScriptable : ScriptableObject
{
    public AccountLevelSetting[] accountLevelSettings;
}

[System.Serializable]
public class AccountLevelSetting
{
    public string name;
    public int numberOfCharacterLevels;
    public GameObject unlockable—haracter;
}
