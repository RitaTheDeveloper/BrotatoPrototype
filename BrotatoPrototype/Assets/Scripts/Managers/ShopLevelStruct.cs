using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLevelStruct : MonoBehaviour
{
    [Tooltip("���� �������� ������:")]
    [SerializeField] public int levelPrice = 0;

    [Tooltip("����� ������ ��������:")]
    [SerializeField] public int levelNumber  = 0;

    [Tooltip("������ �������� ������:")]
    [SerializeField] public List<RareItemsDataStruct> slotsData = new List<RareItemsDataStruct>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
