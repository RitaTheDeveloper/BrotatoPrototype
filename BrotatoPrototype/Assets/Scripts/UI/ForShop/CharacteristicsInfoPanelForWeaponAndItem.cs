using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

public class CharacteristicsInfoPanelForWeaponAndItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiCharscteristicsInfoPrefab;
    [SerializeField] private TextMeshProUGUI uiCharscteristicsNumbersInfoPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private Transform containerForNumbers;

    TextMeshProUGUI[] characteristicsList;
    TextMeshProUGUI[] characteristicsNumbersList;
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
        characteristicsNumbersList = new TextMeshProUGUI[4];

        for (int i = 0; i < characteristicsList.Length; i++)
        {
            var characteristic = Instantiate(uiCharscteristicsInfoPrefab, container);
            characteristicsList[i] = characteristic;
        }        
        for (int i = 0; i < characteristicsNumbersList.Length; i++)
        {
            var characteristicNumber = Instantiate(uiCharscteristicsNumbersInfoPrefab, containerForNumbers);
            characteristicsNumbersList[i] = characteristicNumber;
        }

        GetLocalizeWeapon(characteristicsList);
        characteristicsNumbersList[0].text = "<color=#00864F>" + RoundCharacteristicValue(weapon.StartDamage) + "</color>";
        characteristicsNumbersList[1].text = "<color=#00864F>" + RoundCharacteristicValue(weapon.AttackRange) + "</color>";
        characteristicsNumbersList[2].text = "<color=#00864F>" + RoundCharacteristicValue(weapon.attackPerSecond) + "</color>";
        characteristicsNumbersList[3].text = "<color=#00864F>" + RoundCharacteristicValue(weapon.StartCritChance) * 100f + "</color>" + "%";
    }

    private void GetLocalizeWeapon(TextMeshProUGUI[] characteristics)
    {
        LocalizeStringEvent localize;
        localize = characteristicsList[0].GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");
        localize.SetEntry("damage");
        localize = characteristicsList[1].GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");
        localize.SetEntry("range");
        localize = characteristicsList[2].GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");
        localize.SetEntry("attackSpeedWeapon");
        localize = characteristicsList[3].GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");
        localize.SetEntry("crit");
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
                var characteristicNumber = Instantiate(uiCharscteristicsNumbersInfoPrefab, containerForNumbers);
                LocalizeStringEvent localize;
                localize = characteristic.GetComponent<LocalizeStringEvent>();
                localize.SetTable("UI Text");
                localize.SetEntry(nameAndFloat.Key);
                characteristicNumber.text = "<color=#00864F>" + "+" + nameAndFloat.Value;
            }
            else if (nameAndFloat.Value < 0f)
            {
                var characteristic = Instantiate(uiCharscteristicsInfoPrefab, container);
                var characteristicNumber = Instantiate(uiCharscteristicsNumbersInfoPrefab, containerForNumbers);
                LocalizeStringEvent localize;
                localize = characteristic.GetComponent<LocalizeStringEvent>();
                localize.SetTable("UI Text");
                localize.SetEntry(nameAndFloat.Key);
                characteristicNumber.text = "<color=#BE0A0A>" + nameAndFloat.Value;

            }

        }
    }

    public void DeleteInfo()
    {
        foreach (Transform characteristic in container)
        {
            Destroy(characteristic.gameObject);
        }        
        foreach (Transform characteristic in containerForNumbers)
        {
            Destroy(characteristic.gameObject);
        }
        
    }
}
