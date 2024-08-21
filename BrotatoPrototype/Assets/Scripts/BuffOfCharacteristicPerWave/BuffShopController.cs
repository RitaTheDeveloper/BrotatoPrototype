using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffShopController : MonoBehaviour
{
    [Tooltip("��������� ������� ������ �������������")]
    [SerializeField] public List<ShopLevelStruct> ShopLevelStructsStorage = new List<ShopLevelStruct>();

    // current amount of slots for buffs 
    private int _buffSizeList = 1;

    // current level of characteristicBuffController
    [SerializeField]
    private int _currentBuffShopLevel = 1;

    //��� �����
    private List<UIBuffPerLvl> _allBuffs;

    private Dictionary<int, CharacteristicType> _buffSlots = new Dictionary<int, CharacteristicType>();
    private Dictionary<int, List<UIBuffPerLvl>> _tierToBuff = new Dictionary<int, List<UIBuffPerLvl>>();

    private List<UIBuffPerLvl> buffsForSlots;

    public List<UIBuffPerLvl> AllBuffs { get => _allBuffs; set => _allBuffs = value; }

    private void Start()
    {
       // DistributeBuffsAcrossTiers();
    }

    public void PickBuffsForSale()
    {
        // ���������� ���-�� ������
        _buffSizeList = ShopLevelStructsStorage[_currentBuffShopLevel - 1].slotsData.Count;

        // ����� ���� ������ ������
        // ����� ���� ������ �� ������� �� �����. �������(���, ��� ������)

        // ��� ������� ����� �������� ������
        // ���� ����� ������ ������ - ����� ������� �� �������� ������ ���� �����������
        // ����� �� ���� ������ ���� ����
        // � ���� ������(������) - ������ ������� �� ������� �� ����� (���� ���� (������))(������ �� ����� ������)
        // �� ������ ����� ������ ������� ��� ������ - ����� ������ ������� ������ ��������, � ������� ��� ���� ��� ���-��, � ���������, ���� ����, �� ����� ������
        // ����� ������� �� �����

        buffsForSlots = new List<UIBuffPerLvl>();
        for(int i = 0; i < _buffSizeList; i++)
        {
            List<UIBuffPerLvl> list = _tierToBuff[ShopLevelStructsStorage[_currentBuffShopLevel - 1].slotsData[i].level];
            int randomBuff = Random.Range(0, list.Count);
            buffsForSlots.Add(list[randomBuff]);
        }

    //    foreach(UIBuffPerLvl b in buffsForSlots)
    //    {
    //        Debug.Log()
    //    }
    }

    public void DistributeBuffsAcrossTiers()
    {        
        for(int i = 0; i < _allBuffs.Count; i++)
        {
            if (!_tierToBuff.ContainsKey(_allBuffs[i].tier))
            {
                _tierToBuff[_allBuffs[i].tier] = new List<UIBuffPerLvl>();
            }
            _tierToBuff[_allBuffs[i].tier].Add(_allBuffs[i]);
        }

        PickBuffsForSale();

       // Debug.Log(_tierToBuff[2][1].mainCharacteristic);
    }
}
