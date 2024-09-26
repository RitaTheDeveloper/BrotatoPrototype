using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsOfRace : MonoBehaviour
{
    public bool characterWasUpgraded = false;
    public bool accountWasUpgraded = false;

    ResultOfRace resultOfRace = new ResultOfRace(false, false);
    public int accountLvl;
    public int numberOfWaves;
    private CharacterDataForResults characterData;

    public ResultOfRace ResultOfRace { get => resultOfRace; set => resultOfRace = value; }
    public CharacterDataForResults CharacterData { get => characterData; set => characterData = value; }

    public void CharacterLvlWasUpgraded(bool wasUpgraded, CharacterLevel characterLvl)
    {
        characterWasUpgraded = wasUpgraded;
        resultOfRace.characterLvlUp = true;
        characterData = new CharacterDataForResults();
        characterData.name = characterLvl.gameObject.GetComponent<UiPlayerInfo>().nameHero;
        characterData.lvl = characterLvl.CurrentLvl;
        characterData.numberOfwaves = characterLvl.CurrentNumberOfWavesCompleted;
    }

    public void DefaultResults(CharacterLevel characterLvl)
    {
        numberOfWaves = characterLvl.CurrentNumberOfWavesCompleted;
    }

    public void AccountWasUpgraded(bool wasUpgraded, int lvl)
    {
        accountWasUpgraded = wasUpgraded;
        resultOfRace.accountLvlUp = true;
        accountLvl = lvl;
    }

    public void ResetResults()
    {
        characterWasUpgraded = false;
        accountWasUpgraded = false;
    }

}

[System.Serializable]
public class CharacterDataForResults
{
    public string name;
    public int lvl;
    public int numberOfwaves;
}

[System.Serializable]
public class ResultOfRace
{
    public bool characterLvlUp;
    public bool accountLvlUp;

    public ResultOfRace(bool characterLvlUp, bool accountLvlUp)
    {
        this.characterLvlUp = characterLvlUp;
        this.accountLvlUp = accountLvlUp;
    }
}
