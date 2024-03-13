using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject weaponHoldPrefab;
    [SerializeField] private Transform containerOfWeaponHolds;
    //[SerializeField] private Weapon _startingGun;
    [SerializeField] List<Weapon> listOfWeapons;
    [SerializeField] private float radisOfWeaponHold = 3f;
    [SerializeField] private int maxNumberOfWeapons = 6;
    private Weapon equppiedGun;

    private void Start()
    {
        //if (_startingGun != null)
        //{
        //    EquipGun(_startingGun);
        //}
        //SetWeapons(listOfWeapons);
        //SetWeaponsInWeaponHolders(listOfWeapons);
    }
    //public void EquipGun(Weapon gunToEquip)
    //{
    //    if (equppiedGun != null)
    //    {
    //        Destroy(equppiedGun.gameObject);
    //    }

    //    equppiedGun = Instantiate(gunToEquip, _weaponHolds[0].position, _weaponHolds[0].rotation);
    //    equppiedGun.transform.parent = _weaponHolds[0];
    //}


    //private void DestroyAllWeapons()
    //{
    //    foreach(Transform weaponHolder in _weaponHolds)
    //    {
    //        foreach(Transform child in weaponHolder)
    //        {
    //            Destroy(child.gameObject);
    //        }
    //    }
    //}

    private List<GameObject> CreateListOfWeaponHolds(int amountOfWeapons)
    {
        List<GameObject> listOfWeaponHolds = new List<GameObject>();
        for (int i = 0; i < amountOfWeapons; i++)
        {            
            float angle = 360 / amountOfWeapons * i;
            Vector3 pos = ArithmeticMethods.PointOnTheCircle(containerOfWeaponHolds.transform.position, radisOfWeaponHold, angle);
            GameObject weaponHold = Instantiate(weaponHoldPrefab, pos, Quaternion.identity);
            weaponHold.transform.parent = containerOfWeaponHolds;
            listOfWeaponHolds.Add(weaponHold);
        }

        return listOfWeaponHolds;
    }

    private void SetWeaponsInWeaponHolders(List<Weapon> listOfWeapons)
    {
        List<GameObject> listOfWeaponHolds = CreateListOfWeaponHolds(listOfWeapons.Count);

        for (int i = 0; i < listOfWeaponHolds.Count; i++)
        {
            Weapon newWeapon = Instantiate(listOfWeapons[i]);
            newWeapon.transform.parent = listOfWeaponHolds[i].transform;
            newWeapon.transform.localPosition = listOfWeapons[i].transform.position;
            newWeapon.transform.localRotation = listOfWeapons[i].transform.rotation;
        }
    }
    public void EquipGun(Weapon gunToEquip)
    {
        //TODO магазин передает префаб, надо добавить в список
        listOfWeapons.Add(gunToEquip);
    }

    public void UnequipGun(Weapon gunToUnequip)
    {
        //TODO магазин передает префаб, надо убрать из списка
        listOfWeapons.Remove(gunToUnequip);
    }

    public List<Weapon> GetAllWeapons()
    {
        return listOfWeapons;
    }

    public int GetMaxNumberOfweapons()
    {
        return maxNumberOfWeapons;
    }
}


