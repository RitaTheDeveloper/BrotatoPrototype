using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;


public class UIUnlockedHeroes : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject iconImgPrefab;
    [SerializeField] private Transform containerForIcons;

    public void DisplayUnlockedHeroes(List<GameObject> unlockedPlayers)
    {
        DestroyAllIcons();

        LocalizeStringEvent localize;
        localize = text.GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");

        if (unlockedPlayers.Count > 1)
        {
            localize.SetEntry("Получены новые персонажи");
        }
        else
        {
            localize.SetEntry("Получены новый персонаж");
        }

        for (int i = 0; i < unlockedPlayers.Count; i++)
        {
            GameObject icon = Instantiate(iconImgPrefab, containerForIcons);
            icon.GetComponent<Image>().sprite = unlockedPlayers[i].GetComponent<UiPlayerInfo>().icon;
        }
    }

    public void DestroyAllIcons()
    {
        foreach (Transform icon in containerForIcons)
        {
            Destroy(icon.gameObject);
        }
    }

}
