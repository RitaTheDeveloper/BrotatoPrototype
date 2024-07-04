using UnityEngine;

public interface ICreator 
{
    BaseItem Create(BaseItem baseItem, TierType tier);

    void SetParentItem(BaseItem item, Transform parent);
}
