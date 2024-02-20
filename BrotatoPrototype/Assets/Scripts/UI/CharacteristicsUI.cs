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
    [SerializeField] private TextMeshProUGUI parameterOfDamagePercentage;
    [SerializeField] private TextMeshProUGUI parameterOfMelleeDamage;
    [SerializeField] private TextMeshProUGUI parameterOfRangedDamage;
    [SerializeField] private TextMeshProUGUI parameterOfCritChance;
    [SerializeField] private TextMeshProUGUI parameterOfProbabilityOfDodge;
    [SerializeField] private TextMeshProUGUI parameterOfArmor;
    [SerializeField] private TextMeshProUGUI parameterOfMagnetDistance;

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

        // for damage percentage
        parameterOfDamagePercentage.text = _playerCharacteristics.CurrentDamagePercentage.ToString();

        // for melleeDamage
        parameterOfMelleeDamage.text = _playerCharacteristics.CurrentMelleeDamage.ToString();

        // for rangedDamage
        parameterOfRangedDamage.text = _playerCharacteristics.CurrentRangedDamage.ToString();

        // for critChance
        parameterOfCritChance.text = _playerCharacteristics.CurrentCritChancePercentage.ToString();

        // for Dodge
        parameterOfProbabilityOfDodge.text = _playerCharacteristics.CurrentProbabilityOfDodge.ToString();

        // for Armor
        parameterOfArmor.text = _playerCharacteristics.CurrentArmor.ToString();

        // for Magnet Distance
        parameterOfMagnetDistance.text = _playerCharacteristics.CurrentMagnetDistance.ToString();

    }
}
