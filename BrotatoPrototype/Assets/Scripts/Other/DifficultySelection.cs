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
                Debug.Log("��������� ������");
                break;
            case 1:
                Debug.Log("��������� ����������");
                break;
            case 2:
                Debug.Log("��������� �������");
                break;
            default:
                break;
        }
    }
}