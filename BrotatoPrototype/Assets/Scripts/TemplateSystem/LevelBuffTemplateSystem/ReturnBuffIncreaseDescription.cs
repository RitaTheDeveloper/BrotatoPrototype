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
                result = " изменение сытости";
                break;

            case CharacteristicType.MaxHealth:
                result = " макс. здоровье";
                break;

            case CharacteristicType.RegenerationHP:
                result = " к хп реген. в сек.";
                break;

            case CharacteristicType.Dodge:
                result = "% к уклонению";
                break;

            case CharacteristicType.Armor:
                result = "% к броне";
                break;

            case CharacteristicType.MoveSpeed:
                result = "% к скорости";
                break;

            case CharacteristicType.AttackSpeed:
                result = "% к скорости атаки";
                break;

            case CharacteristicType.Damage:
                result = "% к урону";
                break;

            case CharacteristicType.MeleeDamage:
                result = "% к ближнему урону";
                break;

            case CharacteristicType.RangeDamage:
                result = "% к дальнему урону";
                break;

            case CharacteristicType.ChanceOfCrit:
                result = "% к крит шансу";
                break;

            case CharacteristicType.MagneticRadius:
                result = " к радиусу сбора";
                break;
        }
        return result;
    }
}
