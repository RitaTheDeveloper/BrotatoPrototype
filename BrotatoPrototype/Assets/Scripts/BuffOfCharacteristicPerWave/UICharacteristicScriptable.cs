using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UI For Characteristics Data", menuName = "Data/UI Characteristic")]
public class UICharacteristicScriptable : ScriptableObject
{
    [SerializeField] public UIForCharacteristics[] uiCharacteristics;

    [System.Serializable]
    public class UIForCharacteristics
    {
        public string name;
        public CharacteristicType characteristic;
        public Sprite icon;
        public string description;
    }

    public Sprite GetSprite(CharacteristicType characteristic)
    {
        Sprite icon = null;

        for(int i = 0; i < uiCharacteristics.Length; i++)
        {
            if(uiCharacteristics[i].characteristic == characteristic)
            {
                icon = uiCharacteristics[i].icon;
            }
        }

        return icon;
    }
}
