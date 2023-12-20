using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private int _startLvl = 0;
    public int _currentLvl;
    public float _currentXp;
    public float _requiredXp;
  
    private void Start()
    {
        Init();
    }

    private void Update()
    {
       // IncreaseCurrentExperience(0.01f);
    }

    private void Init()
    {
        _currentLvl = _startLvl;
        _currentXp = 0;
        _requiredXp = GetRequiredXp(_currentLvl);
        DisplayLevel();
    }

    public float GetRequiredXp(int currentLvl)
    {
        return (currentLvl + 3) * (currentLvl + 3);
    }

    private void LevelUp()
    {
        _currentLvl++;
        _currentXp = 0;
        _requiredXp = GetRequiredXp(_currentLvl);

    }

    public void IncreaseCurrentExperience(float xp)
    {
        _currentXp += xp;

        if(_currentXp > _requiredXp)
        {
            LevelUp();
        }
        DisplayLevel();
    }

    private void DisplayLevel()
    {
        var xpPercentage = _currentXp / _requiredXp;
        UIManager.instance.DisplayLevel(_currentLvl, xpPercentage);
    }
}
