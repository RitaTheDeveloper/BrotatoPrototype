using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform _weaponHold;
    [SerializeField] private Weapon _startingGun;
    private Weapon equppiedGun;

    private void Start()
    {
        if (_startingGun != null)
        {
            EquipGun(_startingGun);
        }
    }
    public void EquipGun(Weapon gunToEquip)
    {
        if (equppiedGun != null)
        {
            Destroy(equppiedGun.gameObject);
        }

        equppiedGun = Instantiate(gunToEquip, _weaponHold.position, _weaponHold.rotation);
        equppiedGun.transform.parent = _weaponHold;
    }
}


