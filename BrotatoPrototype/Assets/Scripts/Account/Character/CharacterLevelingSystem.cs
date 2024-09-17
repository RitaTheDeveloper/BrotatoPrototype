using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLevelingSystem : MonoBehaviour
{
    [SerializeField] private CharacterLevelSettingScriptable _characterLevelSetting;

    public CharacterLevelSettingScriptable CharacterLevelSetting { get => _characterLevelSetting; }
}
