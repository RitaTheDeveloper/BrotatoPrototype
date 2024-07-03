using UnityEngine;

public interface IItemBuilder 
{
    Item CreateItem(Item baseItem, TierType tier);

    void SetParentItem(Item item, Transform parent);
}
