using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

public class UIWaveResults : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentHungerTMP;
    [SerializeField] private TextMeshProUGUI amountFoodMinedTMP;
    [SerializeField] private TextMeshProUGUI percentageReductionMaxHealthTMP;
    [SerializeField] private TextMeshProUGUI amountOfLevelUpsForWaveTMP;
    [SerializeField] private TextMeshProUGUI amountGoldForWaveTMP;
    [SerializeField] private TextMeshProUGUI amountWoodForWaveTMP;
    public float percentageReductionMaxHealth;


    public void UpdateWaveResults(PlayerCharacteristics playerCharacteristics)
    {
        float hunger = playerCharacteristics.CurrentHunger;
        currentHungerTMP.text = hunger.ToString();

        int amountOfFoodForWave = playerCharacteristics.GetComponent<PlayerSatiety>().GetAmountOfFoodForWave();
        amountFoodMinedTMP.text = amountOfFoodForWave.ToString();

        amountOfLevelUpsForWaveTMP.text = playerCharacteristics.GetComponent<LevelSystem>().NumberOfLeveledUpForCurrentWave.ToString() + "<sup>õ</sup>";

        float currentSatiety = playerCharacteristics.CurrentSatiety;
        percentageReductionMaxHealth = (float)System.Math.Round(100 - currentSatiety, 1);
        if (percentageReductionMaxHealth < 0) percentageReductionMaxHealth = 0;

        LocalizeStringEvent localize;
        localize = percentageReductionMaxHealthTMP.gameObject.GetComponent<LocalizeStringEvent>();
        localize.RefreshString();

        PlayerInventory inventory = playerCharacteristics.GetComponent<PlayerInventory>();
        int goldForWave = inventory.GetAmountOfMoneyForWave();
        amountGoldForWaveTMP.text = "+" + goldForWave;

        int woodForWave = inventory.GetAmountOfWoodForWave();
        amountWoodForWaveTMP.text = "+" + woodForWave;
    }
}
