using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffShopController : MonoBehaviour
{
    [Tooltip("Структура уровней баффов характеристик")]
    [SerializeField] private List<ShopLevelStruct> _shopLevelStructsStorage = new List<ShopLevelStruct>();

    // current amount of slots for buffs 
    private int _buffSizeList = 1;

    // current level of characteristicBuffController
    [SerializeField]
    private int _currentBuffShopLevel = 1;

    //все баффы
    private List<UIBuffPerLvl> _allBuffs;

    private Dictionary<int, CharacteristicType> _buffSlots = new Dictionary<int, CharacteristicType>();
    private Dictionary<int, List<UIBuffPerLvl>> _tierToBuff = new Dictionary<int, List<UIBuffPerLvl>>();

    private List<UIBuffPerLvl> buffsForSlots;

    public List<UIBuffPerLvl> AllBuffs { get => _allBuffs; set => _allBuffs = value; }
    private List<CharacteristicType> _listOfAlreadySelectedBuffs;

    public void SetCurrentBuffShopLevel()
    {
        PlayerCharacteristics playerCharacteristics = GameManager.instance.player.GetComponent<PlayerCharacteristics>();
        var currentWisdom = playerCharacteristics.CurrentWisdom;
        //if ()
        for (int i = 0; i < _shopLevelStructsStorage.Count; i++)
        {
            if (currentWisdom >= _shopLevelStructsStorage[i].levelPrice)
            {
                _currentBuffShopLevel = _shopLevelStructsStorage[i].levelNumber;
            }
        }
        Debug.Log("buff level = " + _currentBuffShopLevel);

        PickBuffsForSale();
    }

    public void PickBuffsForSale()
    {
        // определяем кол-во слотов
        _buffSizeList = _shopLevelStructsStorage[_currentBuffShopLevel - 1].slotsData.Count;

        // берем весь список абилок
        // делим этот список на словари по тирам. словарь(тир, тир абилки)

        // для каждого слота выбираем абилку
        // бафф левел стракт список - берем элемент по текущему уровню бафф контроллера
        // берем из него список слот дата
        // в слот баффов(индекс) - кладем элемент из словаря по тирам (слот дата (индекс))(рандом из тиров абилок)
        // из списка тиров абилок убираем эту абилку - можно просто создать список индексов, в которых уже есть эта хар-ка, и проверять, если есть, то опять рандом
        // берем словарь по тирам

        buffsForSlots = new List<UIBuffPerLvl>();
        _listOfAlreadySelectedBuffs = new List<CharacteristicType>();
        for (int i = 0; i < _buffSizeList; i++)
        {
            List<UIBuffPerLvl> list = _tierToBuff[_shopLevelStructsStorage[_currentBuffShopLevel - 1].slotsData[i].level];
            int randomBuff = Random.Range(0, list.Count);
            UIBuffPerLvl buff = list[randomBuff];
            while (_listOfAlreadySelectedBuffs.Contains(buff.mainCharacteristic))
            {
                randomBuff = Random.Range(0, list.Count);
                buff = list[randomBuff];
            }
            buffsForSlots.Add(list[randomBuff]);
            _listOfAlreadySelectedBuffs.Add(buff.mainCharacteristic);
        }
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

    }

    public List<UIBuffPerLvl> GetBuffsForSlots()
    {
        return buffsForSlots;
    }
}
