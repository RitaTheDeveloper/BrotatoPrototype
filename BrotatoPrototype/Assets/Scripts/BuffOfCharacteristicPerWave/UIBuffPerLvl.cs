using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuffPerLvl : MonoBehaviour
{
    public CharacteristicType mainCharacteristic;
    public int tier;
    public float value;
    public Sprite icon;
    public RareItemsDataStruct dataTier;

    public void UseBuff()
    {
        GameObject player = GameManager.instance.player;
        player.GetComponent<PlayerCharacteristics>().UpdateCurrentCharacteristic(mainCharacteristic, value);
        UIManager.instance.OkOnClick();
    }
}
