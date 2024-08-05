using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountLevelingSystem : MonoBehaviour
{
    [SerializeField] private AccountLevelSettingScriptable _accountLevelSetting;

    public AccountLevelSettingScriptable AccountLevelSetting { get => _accountLevelSetting; }


}
