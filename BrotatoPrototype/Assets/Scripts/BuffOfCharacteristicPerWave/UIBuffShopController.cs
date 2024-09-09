using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuffShopController : MonoBehaviour
{
    [SerializeField] private BuffShopController _buffShop;
    [SerializeField] private GameObject _proposedBuffPrefab;
    [SerializeField] private Transform _panelOfProposedBuffs;

    List<Transform> listSlotsOfBuffs = new List<Transform>();
    private List<UIBuffPerLvl> _listOfProposedBuffs;

    private void Start()
    {
      //  GetProposedBuffs();
    }

    public void CreateProposedBuffs(List<UIBuffPerLvl> uIBuffPerLvls)
    {
        DestroyAllSlots();

        for (int i = 0; i < uIBuffPerLvls.Count; i++)
        {
            //DestroyAllSlotsForItems();
            GameObject slot = Instantiate(_proposedBuffPrefab, _panelOfProposedBuffs);
            listSlotsOfBuffs.Add(slot.transform);
            slot.GetComponent<ProposedAbility>().SetUIForProposedBuff(uIBuffPerLvls[i]);
        }
    }

    private void SetListOfProposedBuffs()
    {
        _listOfProposedBuffs = new List<UIBuffPerLvl>();
        _listOfProposedBuffs = _buffShop.GetBuffsForSlots();
    }

    public void GetProposedBuffs()
    {
        SetListOfProposedBuffs();
        CreateProposedBuffs(_listOfProposedBuffs);
    }

    private void DestroyAllSlots()
    {
        foreach (Transform child in _panelOfProposedBuffs.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }

        listSlotsOfBuffs.Clear();
    }

}
