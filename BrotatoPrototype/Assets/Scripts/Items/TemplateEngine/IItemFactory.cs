
public interface IItemFactory 
{
    Item CreateItem(Item baseItem, TierType tier);

    // The calculated characteristics from Item.cs must be synchronized with:
    // StandartItem.cs; PlayerCharacteristic.cs; ItemShopInfo.cs;
    void SynchronizeAllCharacteristics(Item item, TierType tier);
}
