using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParametersOfWave
{
    public float waveTime = 30f;
    public int amountOfGoldPerWave = 100;
    public int amountOfExpPerWave = 100;

    public ParametersOfWave(float time, int amountOfGold, int amountOfExp)
    {
        waveTime = time;
        amountOfGoldPerWave = amountOfGold;
        amountOfExpPerWave = amountOfExp;
    }
}
