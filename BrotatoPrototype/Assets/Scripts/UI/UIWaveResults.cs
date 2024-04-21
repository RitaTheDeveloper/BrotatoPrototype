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
        currentHungerTMP.text = playerCharacteristics.CurrentHunger.ToString();

        amountOfLevelUpsForWaveTMP.text = playerCharacteristics.GetComponent<LevelSystem>().NumberOfLeveledUpForCurrentWave.ToString() + "<sup> õ </sup>";
    }
}
