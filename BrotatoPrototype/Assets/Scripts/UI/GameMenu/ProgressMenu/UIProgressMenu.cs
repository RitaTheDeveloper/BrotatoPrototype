using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Components;

public class UIProgressMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberOfWavesPassedTxt;
    [SerializeField] private GameObject _accountObj;
    [SerializeField] private GameObject _characterObj;
    [SerializeField] private TextMeshProUGUI _currentLvlAccount;
    [SerializeField] private TextMeshProUGUI _characterNameTxt;
    [SerializeField] private TextMeshProUGUI _characterLvlTxt;


    public void Init(ResultsOfRace resultsOfrace)
    {
        // _numberOfWavesPassedTxt.text = "�������� " + resultsOfrace.numberOfWaves.ToString() + " ����";
        _numberOfWavesPassedTxt.text = "" + (GameManager.instance.WaveCounter);

        LocalizeStringEvent localize;


        if (resultsOfrace.characterWasUpgraded)
        {
            _characterObj.SetActive(true);

            localize = _characterNameTxt.GetComponent<LocalizeStringEvent>();
            localize.SetTable("UI Text");
            localize.SetEntry(resultsOfrace.CharacterData.name);
            _characterLvlTxt.text = resultsOfrace.CharacterData.lvl.ToString();
           // _numberOfWavesPassedTxt.text = "�������� " + resultsOfrace.CharacterData.numberOfwaves.ToString() + " ����";
        }
        else
        {
            _characterObj.SetActive(false);
        }

        if (resultsOfrace.accountWasUpgraded)
        {
            _accountObj.SetActive(true);
            _currentLvlAccount.text = resultsOfrace.accountLvl.ToString();
        }
        else
        {
            _accountObj.SetActive(false);
        }
    }


}
