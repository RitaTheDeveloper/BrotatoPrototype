using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Localization.Components;

public class CharacteristicsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI parameterOfHunger;
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

    [SerializeField] private TextMeshProUGUI nameHeroTxt;
    private PlayerCharacteristics _playerCharacteristics;

    private void Start()
    {
        // UpdateCharacterisctics();
        if (GameManager.instance.player)
        {
            UpdateCharacterisctics(GameManager.instance.player.GetComponent<PlayerCharacteristics>());
        }
    }
    
    public void UpdateCharacterisctics(PlayerCharacteristics _playerCharacteristics)
    {
        LocalizeStringEvent localize;
        localize = nameHeroTxt.gameObject.GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");
        localize.SetEntry(_playerCharacteristics.GetComponent<UiPlayerInfo>().nameHero);
        localize.RefreshString();

        //for hunger
        parameterOfHunger.text = ((float)Math.Round(_playerCharacteristics.CurrentHunger, 2)).ToString();

        //for maxHp
        parameterOfMaxHp.text = ((float)Math.Round(_playerCharacteristics.CurrentMaxHp, 2)).ToString();
        // for speed
        var parameter = Mathf.Round(_playerCharacteristics.CurrentMoveSpeed * 10) / 10;
        parameterOfSpeed.text = parameter.ToString();

        // for hpRegen
        parameterOfHpRegen.text = ((float)Math.Round(_playerCharacteristics.CurrentHpRegen, 2)).ToString(); 

        // for attackSpeed
        var paramOfAttackSpeed = Mathf.Round(_playerCharacteristics.CurrentAttackSpeedPercentage * 10) / 10;
        parameterOfAttackSpeed.text = paramOfAttackSpeed.ToString();

        // for damage percentage
        parameterOfDamagePercentage.text = ((float)Math.Round(_playerCharacteristics.CurrentDamagePercentage, 2)).ToString();

        // for melleeDamage
        parameterOfMelleeDamage.text = ((float)Math.Round(_playerCharacteristics.CurrentMeleeDamage, 2)).ToString(); 

        // for rangedDamage
        parameterOfRangedDamage.text = ((float)Math.Round(_playerCharacteristics.CurrentRangedDamage, 2)).ToString();

        // for critChance
        parameterOfCritChance.text = ((float)Math.Round(_playerCharacteristics.CurrentCritChancePercentage, 2)).ToString();
        // for Dodge
        parameterOfProbabilityOfDodge.text = ((float)Math.Round(_playerCharacteristics.CurrentProbabilityOfDodge, 2)).ToString();
        // for Armor
        parameterOfArmor.text = ((float)Math.Round(_playerCharacteristics.CurrentArmor, 2)).ToString();

        // for Magnet Distance
        parameterOfMagnetDistance.text = ((float)Math.Round(_playerCharacteristics.CurrentMagnetDistance, 1)).ToString();

    }

    public void HighlightUpgradedCharacteristics(CharacteristicType characteristic, Color color)
    {
        //parameterOfArmor.color = Color.red;
        switch (characteristic)
        {
            case CharacteristicType.Satiety:
                ColorString(parameterOfHunger, color);
                break;

            case CharacteristicType.MaxHealth:
                ColorString(parameterOfMaxHp, color);
                break;

            case CharacteristicType.RegenerationHP:
                ColorString(parameterOfHpRegen, color);
                break;

            case CharacteristicType.Dodge:
                ColorString(parameterOfProbabilityOfDodge, color);
                break;

            case CharacteristicType.Armor:
                ColorString(parameterOfArmor, color);
                break;

            case CharacteristicType.MoveSpeed:
                ColorString(parameterOfSpeed, color);
                break;

            case CharacteristicType.AttackSpeed:
                ColorString(parameterOfAttackSpeed, color);
                break;

            case CharacteristicType.Damage:
                ColorString(parameterOfDamagePercentage, color);
                break;

            case CharacteristicType.MeleeDamage:
                ColorString(parameterOfMelleeDamage, color);
                break;

            case CharacteristicType.RangeDamage:
                ColorString(parameterOfRangedDamage, color);
                break;

            case CharacteristicType.ChanceOfCrit:
                ColorString(parameterOfCritChance, color);
                break;

            case CharacteristicType.MagneticRadius:
                ColorString(parameterOfMagnetDistance, color);
                break;
        }
    }

    private void ColorString(TextMeshProUGUI text, Color color)
    {
        text.color = color;
    }

    public void RemoveCharacteristicsHighlighting()
    {
        Color color = new Color32(53, 27, 26, 255);
        List <TextMeshProUGUI> list = new List<TextMeshProUGUI>();
        list.AddRange(new List<TextMeshProUGUI>() { parameterOfHunger, parameterOfMaxHp, parameterOfSpeed, parameterOfHpRegen, parameterOfAttackSpeed, parameterOfDamagePercentage,
                                                    parameterOfMelleeDamage, parameterOfRangedDamage, parameterOfCritChance, parameterOfProbabilityOfDodge, parameterOfArmor, parameterOfMagnetDistance});

        foreach(var go in list)
        {
            ColorString(go, color);
        }
    }
}
