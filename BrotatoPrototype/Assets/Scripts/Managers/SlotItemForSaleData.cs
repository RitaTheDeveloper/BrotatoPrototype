using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;


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

    private UIShop _uIShop;


    private void Awake()
    {
       // _potAnimator = pot.GetComponentInChildren<Animator>();
    }

    public void Init(UIShop uIShop)
    {
        _uIShop = uIShop;
    }

    public void DisplayInfoForWeapon(ItemShopInfo w, int currentWave)
    {
        LocalizeStringEvent localize;
        localize = textName.GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");
        localize.SetEntry(w.NameWeapon);
        localize.RefreshString();
        if (w.GetComponent<BaseWeapon>().type == BaseWeapon.Type.Melee)
        {
            localize = textType.GetComponent<LocalizeStringEvent>();
            localize.SetTable("UI Text");
            localize.SetEntry("ближний бой");
            localize.RefreshString();
        }
        else
        {
            localize = textType.GetComponent<LocalizeStringEvent>();
            localize.SetTable("UI Text");
            localize.SetEntry("дальний бой");
            localize.RefreshString();
        }

        localize = textTier.GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");
        localize.SetEntry(w.LevelItem.TierString);
        localize.RefreshString();

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

        LocalizeStringEvent localize;
        localize = textName.GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");
        localize.SetEntry(it.ShopInfoItem.NameWeapon);
        localize.RefreshString();

        localize = textType.GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");
        localize.SetEntry("снаряжение");
        localize.RefreshString();

        localize = textTier.GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");
        localize.SetEntry(it.ShopInfoItem.LevelItem.TierString);
        localize.RefreshString();

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
       // UIShop.instance.ButtonBuySlot(SlotNumber);
        _uIShop.ButtonBuySlot(SlotNumber);
    }

    public void PotOff()
    {
        pot.SetActive(false);
        buyBtn.gameObject.SetActive(false);
        textName.gameObject.SetActive(false);
        textTier.gameObject.SetActive(false);
        textType.gameObject.SetActive(false);
        characteristicsInfo.DeleteInfo();
    }

    public void PotOn()
    {
        pot.SetActive(true);
        textName.gameObject.SetActive(true);
        textTier.gameObject.SetActive(true);
        textType.gameObject.SetActive(true);
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
        //ImageAlphaOff();
        //_potAnimator.gameObject.GetComponent<Image>().sprite = effectSprite1;
        //_potAnimator.gameObject.SetActive(false);
    }
}
