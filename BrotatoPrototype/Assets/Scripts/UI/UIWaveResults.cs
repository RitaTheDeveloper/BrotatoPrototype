using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWaveResults : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentHungerTMP;
    [SerializeField] private TextMeshProUGUI amountFoodMinedTMP;
    [SerializeField] private TextMeshProUGUI percentageReductionMaxHealthTMP;
    [SerializeField] private TextMeshProUGUI amountOfLevelUpsForWaveTMP;
    [SerializeField] private TextMeshProUGUI amountGoldForWaveTMP;
    [SerializeField] private TextMeshProUGUI amountWoodForWaveTMP;


    public void UpdateWaveResults(PlayerCharacteristics playerCharacteristics)
    {
        int hunger = playerCharacteristics.CurrentHunger;
        currentHungerTMP.text = hunger.ToString();

        int amountOfFoodForWave = playerCharacteristics.GetComponent<PlayerSatiety>().GetAmountOfFoodForWave();
        amountFoodMinedTMP.text = amountOfFoodForWave.ToString();

        amountOfLevelUpsForWaveTMP.text = playerCharacteristics.GetComponent<LevelSystem>().NumberOfLeveledUpForCurrentWave.ToString() + "<sup>�</sup>";

        int currentSatiety = playerCharacteristics.CurrentSatiety;
        int percentageReductionMaxHealth = 100 - currentSatiety;
        if (percentageReductionMaxHealth < 0) percentageReductionMaxHealth = 0;

        percentageReductionMaxHealthTMP.text = "������������ �������� \n ������� �� " + percentageReductionMaxHealth + "%";

        PlayerInventory inventory = playerCharacteristics.GetComponent<PlayerInventory>();
        int goldForWave = inventory.GetAmountOfMoneyForWave();
        amountGoldForWaveTMP.text = "+" + goldForWave;

        int woodForWave = inventory.GetAmountOfWoodForWave();
        amountWoodForWaveTMP.text = "+" + woodForWave;
    }
}