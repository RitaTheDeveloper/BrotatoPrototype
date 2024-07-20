using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacteristicsInfoPanelForWeaponAndItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiCharscteristicsInfoPrefab;
    [SerializeField] private Transform container;

    TextMeshProUGUI[] characteristicsList;
    public void SetDescriptionOfCharacteristics(ItemShopInfo itemInfo)
    {
        DeleteInfo();
        
        BaseWeapon weapon = itemInfo.GetComponent<BaseWeapon>();
        //для оружий
        if (weapon)
        {
            DisplayCharacteristicsForWeapon(weapon);
        }

        PlayerCharacteristics characteristics = itemInfo.GetComponent<PlayerCharacteristics>();
        // для предметов
        if (characteristics)
        {
            DisplayCharacteristicsForItem(characteristics);
        }
    }

    private void DisplayCharacteristicsForWeapon(BaseWeapon weapon)
    {
        characteristicsList = new TextMeshProUGUI[4];
        for (int i = 0; i < characteristicsList.Length; i++)
        {
            var characteristic = Instantiate(uiCharscteristicsInfoPrefab, container);
            characteristicsList[i] = characteristic;
        }
        characteristicsList[0].text = "Урон: " + "<color=#00864F>" + RoundCharacteristicValue(weapon.StartDamage) + "</color>";
        characteristicsList[1].text = "Дальность: " + "<color=#00864F>" + RoundCharacteristicValue(weapon.AttackRange) + "</color>";
        characteristicsList[2].text = "Скорость атаки: " + "<color=#00864F>" + RoundCharacteristicValue(weapon.attackPerSecond) + "</color>";
        characteristicsList[3].text = "Крит шанс: " + "<color=#00864F>" + RoundCharacteristicValue(weapon.StartCritChance) * 100f + "</color>" + "%";
    }

    private float RoundCharacteristicValue(float number)
    {
        return Mathf.Round(number * 100f) / 100f;
    }

    private void DisplayCharacteristicsForItem(PlayerCharacteristics characteristics)
    {
        Dictionary<string, float> notEmptyValuesList = characteristics.DecriptionChacteristicsForUIShop();

        characteristicsList = new TextMeshProUGUI[notEmptyValuesList.Count];

        foreach (KeyValuePair<string, float> nameAndFloat in notEmptyValuesList)
        {
            if (nameAndFloat.Value > 0f)
            {
                var characteristic = Instantiate(uiCharscteristicsInfoPrefab, container);
                characteristic.text = "<color=#00864F>" + "+" + nameAndFloat.Value + "</color>" + nameAndFloat.Key;
            }
            else if (nameAndFloat.Value < 0f)
            {
                var characteristic = Instantiate(uiCharscteristicsInfoPrefab, container);
                characteristic.text = "<color=#BE0A0A>" + nameAndFloat.Value + "</color>" + nameAndFloat.Key;
            }

        }
    }

    public void DeleteInfo()
    {
        foreach (Transform characteristic in container)
        {
            Destroy(characteristic.gameObject);
        }
    }
}
