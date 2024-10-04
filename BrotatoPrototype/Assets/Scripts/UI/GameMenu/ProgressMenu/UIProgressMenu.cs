using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        // _numberOfWavesPassedTxt.text = "пройдено " + resultsOfrace.numberOfWaves.ToString() + " волн";
        _numberOfWavesPassedTxt.text = "пройдено волн: " + (GameManager.instance.WaveCounter);

        if (resultsOfrace.characterWasUpgraded)
        {
            _characterObj.SetActive(true);

            _characterNameTxt.text = resultsOfrace.CharacterData.name;
            _characterLvlTxt.text = resultsOfrace.CharacterData.lvl.ToString();
           // _numberOfWavesPassedTxt.text = "пройдено " + resultsOfrace.CharacterData.numberOfwaves.ToString() + " волн";
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
