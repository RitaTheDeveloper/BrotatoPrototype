using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotItemForSaleData : MonoBehaviour
{
    public int SlotNumber;
    public string SlotEntytiID;
    public GameObject pot;
    public Sprite effectSprite1;
    [SerializeField] private Animator _potAnimator;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textTier;
    public TextMeshProUGUI textType;        
    public TextMeshProUGUI textCost;
    public Image image;
    public TextMeshProUGUI lockButtonText;
    public Button buyBtn;
    [SerializeField] private CharacteristicsInfoPanelForWeaponAndItem characteristicsInfo;


    private void Awake()
    {
       // _potAnimator = pot.GetComponentInChildren<Animator>();
    }

    public void DisplayInfoForWeapon(ItemShopInfo w, int currentWave)
    {
        textName.text = w.NameWeapon;
        if (w.GetComponent<Weapon>().type == Weapon.Type.Melee)
        {
            textType.text = "ближний бой";
        }
        else
        {
            textType.text = "дальний бой";
        }
        textTier.text = w.LevelItem.TierString;

        textCost.text = w.GetPrice(currentWave).ToString();
        _potAnimator.gameObject.SetActive(true);
        ImageAlphaOff();
        _potAnimator.SetTrigger("change");
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ChangeSprite(w.IconWeapon));
        }
        else
        {
            image.sprite = w.IconWeapon;
        }
        
        //ChangeSprite(w.IconWeapon);
       // image.sprite = w.IconWeapon;
        SetCharacteristicsInfo(w);
        buyBtn.onClick.RemoveAllListeners();
        OnClickBuyItem();
    }

    public void DisplayInfoForItem(StandartItem it, int currentWave)
    {
        textName.text = it.ShopInfoItem.NameWeapon;
        textType.text = "снаряжение";
        textTier.text = it.ShopInfoItem.LevelItem.TierString;
        textCost.text = it.ShopInfoItem.GetPrice(currentWave).ToString();
        _potAnimator.gameObject.SetActive(true);
        ImageAlphaOff();
        _potAnimator.SetTrigger("change");
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ChangeSprite(it.ShopInfoItem.IconWeapon));
        }
        else
        {
            image.sprite = it.ShopInfoItem.IconWeapon;
        }
           // StartCoroutine(ChangeSprite(it.ShopInfoItem.IconWeapon));
        //image.sprite = it.ShopInfoItem.IconWeapon;
        SetCharacteristicsInfo(it.GetComponent<ItemShopInfo>());
        buyBtn.onClick.RemoveAllListeners();
        OnClickBuyItem();
    }

    public void OnClickBuyItem()
    {
        buyBtn.onClick.AddListener(OnBuyItem);
    }

    public void OnBuyItem()
    {
        Debug.Log("хочу купить");
        UIShop.instance.ButtonBuySlot(SlotNumber);
    }

    public void PotOff()
    {
        pot.SetActive(false);
        buyBtn.gameObject.SetActive(false);
        textName.text = "";
        textTier.text = "";
        textType.text = "";
        characteristicsInfo.DeleteInfo();
    }

    public void PotOn()
    {
        pot.SetActive(true);
        buyBtn.gameObject.SetActive(true);
    }   

    public void SetCharacteristicsInfo(ItemShopInfo itemInfo)
    {
        characteristicsInfo.SetDescriptionOfCharacteristics(itemInfo);
    }

    private void ImageAlphaOff()
    {
        _potAnimator.gameObject.GetComponent<Image>().sprite = effectSprite1;
        var color = _potAnimator.gameObject.GetComponent<Image>().color;
        color.a = 0f;
        _potAnimator.gameObject.GetComponent<Image>().color = color;
    }

    private IEnumerator ChangeSprite(Sprite newSprite)
    {
        yield return new WaitForSeconds(0.25f);
        image.sprite = newSprite;
        yield return new WaitForSeconds(0.6f);
        _potAnimator.gameObject.GetComponent<Image>().sprite = effectSprite1;
        _potAnimator.gameObject.SetActive(false);
        
    }
}
