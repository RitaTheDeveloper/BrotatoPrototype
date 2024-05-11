using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgrateShopBtn : MonoBehaviour
{
    [SerializeField] private GameObject defaultTxt;
    [SerializeField] private GameObject img;
    [SerializeField] private GameObject maxShopTxt;

    public void ShopMax(bool shopIsMax)
    {
        defaultTxt.SetActive(!shopIsMax);
        img.SetActive(!shopIsMax);
        maxShopTxt.SetActive(shopIsMax);
        gameObject.GetComponent<Button>().interactable = !shopIsMax;
    }

}
