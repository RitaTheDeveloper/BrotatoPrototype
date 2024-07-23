using UnityEngine;

public interface ICreator 
{
    Item CreateItem(Item baseItem, TierType tier);

    Weapon CreateWeapon(Weapon baseWeapon, TierType tier);
}
