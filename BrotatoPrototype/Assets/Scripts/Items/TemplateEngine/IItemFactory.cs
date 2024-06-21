
using UnityEngine;

public interface IItemFactory 
{
    Item CreateItem(Item baseItem, TierType tier);

    void SetParentItem(Item item, Transform parent);
}
