using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacteristicsInfoPanelForWeaponAndItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiCharscteristicsInfoPrefab;
    [SerializeField] private Transform container;


    public void SetDescriptionOfCharacteristics(ItemShopInfo itemInfo)
    {
        DeleteInfo();
        TextMeshProUGUI[] characteristicsList;
        Weapon weapon = itemInfo.GetComponent<Weapon>();

        if (weapon)
        {
            characteristicsList = new TextMeshProUGUI[4];
            for (int i = 0; i < characteristicsList.Length; i++)
            {
                var characteristic = Instantiate(uiCharscteristicsInfoPrefab, container);
                characteristicsList[i] = characteristic;
            }
            characteristicsList[0].text = "Урон: " + "<color=#00864F>" + weapon.StartDamage + "</color>";
            characteristicsList[1].text = "Дальность: " + "<color=#00864F>" + weapon.AttackRange + "</color>";
            characteristicsList[2].text= "Скорость атаки: " + "<color=#00864F>" + weapon.StartAttackSpeed + "</color>";
            characteristicsList[3].text = "Крит шанс: " + "<color=#00864F>" + weapon.StartCritChance * 100f  + "</color>" + "%";
        }

        PlayerCharacteristics characteristics = itemInfo.GetComponent<PlayerCharacteristics>();

        if (characteristics)
        {
            Dictionary<string, float> notEmptyValuesList = characteristics.DecriptionChacteristicsForUIShop();

            characteristicsList = new TextMeshProUGUI[notEmptyValuesList.Count];
            //for (int i = 0; i < notEmptyValuesList.Count; i++)
            //{
            //    var characteristic = Instantiate(uiCharscteristicsInfoPrefab, container);
            //    characteristicsList[i] = characteristic;
            //    characteristicsList[i].text = notEmptyValuesList[i].K
            //}

            foreach (KeyValuePair<string, float> nameAndFloat in notEmptyValuesList)
            {
                if(nameAndFloat.Value > 0f)
                {
                    var characteristic = Instantiate(uiCharscteristicsInfoPrefab, container);
                    characteristic.text = "<color=#00864F>" + "+" +  nameAndFloat.Value + "</color>" + nameAndFloat.Key;
                }
                else if(nameAndFloat.Value < 0f)
                {
                    var characteristic = Instantiate(uiCharscteristicsInfoPrefab, container);
                    characteristic.text = "<color=#BE0A0A>"  + nameAndFloat.Value + "</color>" + nameAndFloat.Key;
                }
                
            }
        }
    }

    private void DeleteInfo()
    {
        foreach (Transform characteristic in container)
        {
            Destroy(characteristic.gameObject);
        }
    }
}
