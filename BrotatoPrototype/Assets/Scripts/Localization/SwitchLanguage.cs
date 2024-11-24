using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Components;

public class SwitchLanguage : MonoBehaviour
{
    private SaveController _saveController;
    private GameManager _gameManager;


    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
        Debug.Log("_gameManager" + _gameManager);
        _saveController = gameManager.GetComponent<SaveController>();
        SetLocalization();
    }

    private void SetLocalization()
    {
        string localization;
        localization = _saveController.GetSelectedLocalization();
        
        if (localization == "russian")
        {
            SetRussianLocalization();
        }

        else if (localization == "english")
        {
            SetEnglishLocalization();
        }
    }

    public void SetEnglishLocalization()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        _saveController.SaveSelectedLocalization("english");
    }

    public void SetRussianLocalization()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        _saveController.SaveSelectedLocalization("russian");
    }
}
