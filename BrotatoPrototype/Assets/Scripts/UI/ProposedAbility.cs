using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProposedAbility : MonoBehaviour
{
    [SerializeField] private Image iconImg;
    [SerializeField] public Ability ability = null;
    [SerializeField] private Button okBtn;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI descriptionTxt;


    private void Start()
    {
        SetUIForProposedAbility();
    }

    public void SetUIForProposedAbility()
    {
        iconImg.sprite = ability.GetSprite();
        nameTxt.text = ability.GetName();
        descriptionTxt.text = ability.GetDescription();
        okBtn.onClick.RemoveAllListeners();
        okBtn.onClick.AddListener(ability.UseAbility);
    }
}
