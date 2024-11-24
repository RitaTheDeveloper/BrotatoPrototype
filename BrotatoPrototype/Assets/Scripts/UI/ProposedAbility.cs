using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class ProposedAbility : MonoBehaviour
{
    [SerializeField] private Image iconImg;
    [SerializeField] private Image background;
    [SerializeField] private Button okBtn;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI descriptionTxt;
    [SerializeField] private TextMeshProUGUI valueTxt;

    public void SetUIForProposedBuff(UIBuffPerLvl uiBuff)
    {
        iconImg.sprite = uiBuff.icon;
        background.sprite = uiBuff.dataTier.backgroundSprite;
        ReturnBuffIncreaseDescription buffDes = new ReturnBuffIncreaseDescription();

        LocalizeStringEvent localize;
        localize = descriptionTxt.GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");
        localize.SetEntry(buffDes.BuffIncreaseDescription(uiBuff.mainCharacteristic));
        localize.RefreshString();
        descriptionTxt.text = descriptionTxt.text + "<color=#00864F>" + " +" + uiBuff.value + "</color>";

        //valueTxt.text = "";
        okBtn.onClick.RemoveAllListeners();
        okBtn.onClick.AddListener(uiBuff.UseBuff);
    }
}
