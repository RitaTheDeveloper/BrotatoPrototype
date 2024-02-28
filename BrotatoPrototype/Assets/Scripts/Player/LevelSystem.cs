using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private int _startLvl = 0;
    [SerializeField] private float _maxMagnetDistance = 20f;
    public int _currentLvl;
    public float _currentXp;
    public float _requiredXp;
    public float _magnetDistance = 3f;
    private int numberOfLeveledUpForCurrentWave;

    private PlayerCharacteristics playerCharacteristics;
    private CurrencyController currencyController;

    public int NumberOfLeveledUpForCurrentWave { get => numberOfLeveledUpForCurrentWave; set => numberOfLeveledUpForCurrentWave = value; }

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
        currencyController = GameObject.FindWithTag("GameManager").GetComponent<CurrencyController>();
        playerCharacteristics = GetComponent<PlayerCharacteristics>();
        SetMagnetDistance();
        _currentLvl = _startLvl;
        _currentXp = 0;
        numberOfLeveledUpForCurrentWave = 0;
        _requiredXp = GetRequiredXp(_currentLvl);
        DisplayLevel();
    }

    private void ConsumeAllCurrencyInRange(float range)
    {
        Vector3 playerPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        Collider[] hitColliders = Physics.OverlapSphere(playerPosition, range);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == "Currency")
            {
                Transform currency = hitColliders[i].gameObject.transform;
                Vector3 targetPosition = new Vector3(transform.position.x, currency.transform.position.y, transform.position.z);
                currency.position = Vector3.MoveTowards(currency.position, targetPosition, 40f * Time.deltaTime);

                if (Vector3.Distance(currency.position, targetPosition) < 1f)
                {
                    var xp = currency.GetComponent<Currency>().GetXP();
                    currencyController.ChangeTotalAmountOfCurrency(1);
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
        numberOfLeveledUpForCurrentWave++;
        playerCharacteristics.LevelUp();
        UIManager.instance.DisplayLevelUp();
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

    public void SetMagnetDistance()
    {
        _magnetDistance = playerCharacteristics.CurrentMagnetDistance;
        if (_magnetDistance < 1f)
        {
            _magnetDistance = 1f;
        }
        else if(_magnetDistance > _maxMagnetDistance)
        {
            _magnetDistance = _maxMagnetDistance;
        }

    }
}
