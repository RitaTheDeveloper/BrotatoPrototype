using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIAccountProgressMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _accountLvlTxt;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UIAccountProgressBar accountProgressBar;

    private AccountLevel _accountLevel;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _accountLevel = _gameManager.AccountLevel;
        accountProgressBar.CreateProgressBar(_accountLevel.AccountLevelSettings, _accountLevel.GetSumOfLvlsOfOpenCharacters());
        SetAccontLvl();
    }

    public void SetAccontLvl()
    {
        _accountLvlTxt.text = _accountLevel.CurrentLvl.ToString();
    }

    public void OnClickBackBtn()
    {
        gameObject.SetActive(false);
    }

}
