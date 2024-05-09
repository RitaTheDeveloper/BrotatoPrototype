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
    List<GameObject> listOfWeaponHolds;

    private void Start()
    {       
        EquipPlayer();
    }

    private void DestroyAllWeapons()
    {
        foreach (Transform weaponHolder in containerOfWeaponHolds)
        {
           
            Destroy(weaponHolder.gameObject);
        }
    }

    private List<GameObject> CreateListOfWeaponHolds(int amountOfWeapons)
    {
        listOfWeaponHolds = new List<GameObject>();
        for (int i = 0; i < amountOfWeapons; i++)
        {            
            float angle = 360 / amountOfWeapons * i;
            angle += 90;
            Vector3 pos = ArithmeticMethods.PointOnTheCircle(containerOfWeaponHolds.transform.position, radisOfWeaponHold, angle);
            GameObject weaponHold = Instantiate(weaponHoldPrefab, pos, Quaternion.identity);
            weaponHold.transform.parent = containerOfWeaponHolds;
            listOfWeaponHolds.Add(weaponHold);
        }

        return listOfWeaponHolds;
    }

    public void SetWeaponsInWeaponHolders(List<Weapon> listOfWeapons)
    {
        DestroyAllWeapons();

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

    public void EquipPlayer()
    {
        SetWeaponsInWeaponHolders(listOfWeapons);
    }
}


