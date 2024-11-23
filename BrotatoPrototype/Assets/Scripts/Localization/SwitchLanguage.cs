using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Components;

public class SwitchLanguage : MonoBehaviour
{
    private void Start()
    {
        SetRussianLocalization();
    }

    public void SetEnglishLocalization()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
    }

    public void SetRussianLocalization()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
    }
}
