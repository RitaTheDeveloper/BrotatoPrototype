using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPhrasesLevel : MonoBehaviour
{
    [Header("������� ����")]
    [SerializeField] public int phrasesLevel;

    [Header("IDLE �����")]
    [SerializeField] public List<Sound> idlePhrases;

    [Header("ShopIn �����")]
    [SerializeField] public List<Sound> shopInPhrases;

    [Header("ShopOut �����")]
    [SerializeField] public List<Sound> shopOutPhrases;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
