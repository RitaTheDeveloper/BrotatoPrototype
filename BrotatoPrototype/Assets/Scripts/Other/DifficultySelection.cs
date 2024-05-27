using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelection: MonoBehaviour
{
    private int _difficulty = 0;
    public void OnClickSelectDifficulty(int difficulty)
    {
        _difficulty = difficulty;

        GameManager.instance.CurrentDifficulty = _difficulty;        
    }
}