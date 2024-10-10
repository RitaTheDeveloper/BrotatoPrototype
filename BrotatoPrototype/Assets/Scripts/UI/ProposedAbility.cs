using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProposedAbility : MonoBehaviour
{
    [SerializeField] private Image iconImg;
    [SerializeField] private Image background;
    [SerializeField] private Button okBtn;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI descriptionTxt;

    public void SetUIForProposedBuff(UIBuffPerLvl uiBuff)
    {
        iconImg.sprite = uiBuff.icon;
        background.sprite = uiBuff.dataTier.backgroundSprite;
        ReturnBuffIncreaseDescription buffDes = new ReturnBuffIncreaseDescription();
        descriptionTxt.text = "+ " + uiBuff.value + buffDes.BuffIncreaseDescription(uiBuff.mainCharacteristic);
        okBtn.onClick.RemoveAllListeners();
        okBtn.onClick.AddListener(uiBuff.UseBuff);
    }
}
