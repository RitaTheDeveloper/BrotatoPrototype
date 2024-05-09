using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIUnlockedHeroes : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Transform containerForIcons;

    public void DisplayUnlockedHeroes(List<GameObject> unlockedPlayers)
    {
        DestroyAllIcons();

        if(unlockedPlayers.Count > 1)
        {
            text.text = "Получены новые персонажи";
        }
        else
        {
            text.text = "Получен новый персонаж";
        }

        for(int i = 0; i < unlockedPlayers.Count; i++)
        {
            Image icon = Instantiate(gameObject.AddComponent<Image>(), containerForIcons);
            icon.sprite = unlockedPlayers[i].GetComponent<UiPlayerInfo>().icon;
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
