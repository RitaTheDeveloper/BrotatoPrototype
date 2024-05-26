using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelection: MonoBehaviour
{
    private int _difficulty = 0;
    public void OnClickSelectDifficulty(int difficulty)
    {
        _difficulty = difficulty;

        switch (difficulty)
        {
            case 0:                
                Debug.Log("сложность легкая");
                break;
            case 1:
                Debug.Log("сложность нормальная");
                break;
            case 2:
                Debug.Log("сложность тяжелая");
                break;
            default:
                break;
        }
    }
}