using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class CharacteristicsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI parameterOfMaxHp;
    [SerializeField] private TextMeshProUGUI parameterOfSpeed;
    [SerializeField] private TextMeshProUGUI parameterOfHpRegen;
    [SerializeField] private TextMeshProUGUI parameterOfAttackSpeed;

    private PlayerCharacteristics _playerCharacteristics;

    private void Start()
    {        
        UpdateCharacterisctics();
    }

    public void UpdateCharacterisctics()
    {
        _playerCharacteristics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacteristics>();

        //for maxHp
        parameterOfMaxHp.text = _playerCharacteristics.CurrentMaxHp.ToString();

        // for speed
        var parameter = Mathf.Round(_playerCharacteristics.CurrentMoveSpeed * 10) / 10;
        parameterOfSpeed.text = parameter.ToString();

        // for hpRegen
        parameterOfHpRegen.text = _playerCharacteristics.CurrentHpRegen.ToString();

        // for attackSpeed
        var paramOfAttackSpeed = Mathf.Round(_playerCharacteristics.CurrentAttackSpeedPercentage * 10) / 10;
        parameterOfAttackSpeed.text = paramOfAttackSpeed.ToString();
    }
}
