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
                result = " ��������� �������";
                break;

            case CharacteristicType.MaxHealth:
                result = " ����. ��������";
                break;

            case CharacteristicType.RegenerationHP:
                result = " � �� �����. � ���.";
                break;

            case CharacteristicType.Dodge:
                result = "% � ���������";
                break;

            case CharacteristicType.Armor:
                result = "% � �����";
                break;

            case CharacteristicType.MoveSpeed:
                result = "% � ��������";
                break;

            case CharacteristicType.AttackSpeed:
                result = "% � �������� �����";
                break;

            case CharacteristicType.Damage:
                result = "% � �����";
                break;

            case CharacteristicType.MeleeDamage:
                result = "% � �������� �����";
                break;

            case CharacteristicType.RangeDamage:
                result = "% � �������� �����";
                break;

            case CharacteristicType.ChanceOfCrit:
                result = "% � ���� �����";
                break;

            case CharacteristicType.MagneticRadius:
                result = " � ������� �����";
                break;
        }
        return result;
    }
}
