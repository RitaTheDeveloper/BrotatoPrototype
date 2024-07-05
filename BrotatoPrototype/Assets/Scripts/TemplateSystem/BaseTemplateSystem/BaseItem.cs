using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    [Header("Name")]
    [SerializeField] protected string inGameNameT1_3;
    [SerializeField] protected string inGameNameT4;
    [Header("Icon")]
    [SerializeField] protected Sprite iconT1;
    [SerializeField] protected Sprite iconT2;
    [SerializeField] protected Sprite iconT3;
    [SerializeField] protected Sprite iconT4;

    [Space]
    [Header("Characteristics")]
    [SerializeField] protected CharacteristicValues characteristicValues;

    [ReadOnlyInspector] public TierType tier = TierType.FirstTier;
    [HideInInspector] public string gameName;
    [HideInInspector] public string editorName;
    [HideInInspector] public Sprite icon;
    protected Dictionary<CharacteristicType, float> characteristicMap = new Dictionary<CharacteristicType, float>();

    public BaseItem Initialize(TierType tier)
    {
        this.tier = tier;

        switch (tier)
        {
            case TierType.FirstTier:
                gameName = inGameNameT1_3;
                icon = iconT1;
                break;

            case TierType.SecondTier:
                gameName = inGameNameT1_3;
                icon = iconT2;
                break;

            case TierType.ThirdTier:
                gameName = inGameNameT1_3;
                icon = iconT3;
                break;

            case TierType.FourthTier:
                gameName = inGameNameT4;
                icon = iconT4;
                break;

        }

        AddSuffixToEditorName(tier);

        BaseItem instancedItem = Instantiate(this);
        RenameInstance(instancedItem, editorName);
        instancedItem.CalculateAllCharacteristics();
        instancedItem.SynchronizeComponents();
        return instancedItem;
    }

    private void AddSuffixToEditorName(TierType tier)
    {
        editorName = gameObject.name;

        editorName = editorName.Remove(editorName.Length - 3);

        switch (tier)
        {
            case TierType.FirstTier:
                editorName = editorName + "_T1";
                break;
            case TierType.SecondTier:
                editorName = editorName + "_T2";
                break;
            case TierType.ThirdTier:
                editorName = editorName + "_T3";
                break;
            case TierType.FourthTier:
                editorName = editorName + "_T4";
                break;
        }
    }

    private void RenameInstance(BaseItem item, string newEditorName)
    {
        item.gameObject.name = newEditorName;
    }

    public abstract void SynchronizeComponents();

    protected abstract void SynchronizeItemShopInfo();

    protected abstract void CalculateAllCharacteristics();
}
