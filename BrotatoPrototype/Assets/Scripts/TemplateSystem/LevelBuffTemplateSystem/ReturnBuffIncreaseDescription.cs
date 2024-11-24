using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBuffIncreaseDescription
{
    public string BuffIncreaseDescription(CharacteristicType characteristic)
    {
        string result = "";
        switch (characteristic)
        {
            case CharacteristicType.Satiety:
                result = "сытость волна";
                break;

            case CharacteristicType.MaxHealth:
                result = "Макс здровье";
                break;

            case CharacteristicType.RegenerationHP:
                result = "хп реген в сек";
                break;

            case CharacteristicType.Dodge:
                result = "уклонение";
                break;

            case CharacteristicType.Armor:
                result = "% брони";
                break;

            case CharacteristicType.MoveSpeed:
                result = "% к скорости";
                break;

            case CharacteristicType.AttackSpeed:
                result = "attackSpeed";
                break;

            case CharacteristicType.Damage:
                result = "% к урону";
                break;

            case CharacteristicType.MeleeDamage:
                result = "meleeDamage";
                break;

            case CharacteristicType.RangeDamage:
                result = "урон в дальнем бою";
                break;

            case CharacteristicType.ChanceOfCrit:
                result = "% для крит удара";
                break;

            case CharacteristicType.MagneticRadius:
                result = "радиус сбора";
                break;
        }
        return result;
    }
}
