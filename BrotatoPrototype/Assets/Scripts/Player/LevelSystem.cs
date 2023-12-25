using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private int _startLvl = 0;
    public int _currentLvl;
    public float _currentXp;
    public float _requiredXp;
    public float _magnetDistance = 3f;
  
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        ConsumeAllCurrencyInRange(_magnetDistance);        
    }

    private void Init()
    {
        _currentLvl = _startLvl;
        _currentXp = 0;
        _requiredXp = GetRequiredXp(_currentLvl);
        DisplayLevel();
    }

    private void ConsumeAllCurrencyInRange(float range)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == "Currency")
            {
                Transform currency = hitColliders[i].gameObject.transform;
                Vector3 targetPosition = new Vector3(transform.position.x, currency.transform.position.y, transform.position.z);
                currency.position = Vector3.MoveTowards(currency.position, targetPosition, 40f * Time.deltaTime);

                if (Vector3.Distance(currency.position, transform.position) < 3f)
                {
                    var xp = currency.GetComponent<Currency>().GetXP();
                    IncreaseCurrentExperience(xp);
                    currency.GetComponent<Currency>().PutAwayFromScene();
                }
            }
        }
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
