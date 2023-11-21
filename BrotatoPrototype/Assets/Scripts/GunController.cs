using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform _weaponHold;
    [SerializeField] private Gun _startingGun;
    private Gun equppiedGun;

    private void Start()
    {
        if (_startingGun != null)
        {
            EquipGun(_startingGun);
        }
    }
    public void EquipGun(Gun gunToEquip)
    {
        if (equppiedGun != null)
        {
            Destroy(equppiedGun.gameObject);
        }

        equppiedGun = Instantiate(gunToEquip, _weaponHold.position, _weaponHold.rotation);
        equppiedGun.transform.parent = _weaponHold;
    }
}
