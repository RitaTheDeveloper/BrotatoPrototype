using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAbilities : MonoBehaviour
{
    [SerializeField] private List<Ability> allAbilities;

    [SerializeField] private ProposedAbility[] proposedAbilities;

    private List<Ability> _remainingAbilities;

    private void Awake()
    {
        ChooseAbilitiesForProposeAbilities();
    }

    public void ChooseAbilitiesForProposeAbilities()
    {
        _remainingAbilities = new List<Ability>();
        _remainingAbilities.AddRange(allAbilities.ToArray());

        for (int i = 0; i < proposedAbilities.Length; i++)
        {
            int randIndex = (int)Random.Range(0, _remainingAbilities.Count);
            proposedAbilities[i].ability = _remainingAbilities[randIndex];
            proposedAbilities[i].SetUIForProposedAbility();
            _remainingAbilities.Remove(_remainingAbilities[randIndex]);
        }                
    }
}
