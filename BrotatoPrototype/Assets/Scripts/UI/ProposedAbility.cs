using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProposedAbility : MonoBehaviour
{
    [SerializeField] private Ability ability;
    [SerializeField] private Button okBtn;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI descriptionTxt;


    private void Start()
    {
        nameTxt.text = ability.GetName();
        descriptionTxt.text = ability.GetDescription();
        okBtn.onClick.AddListener(ability.UseAbility);
    }
}
