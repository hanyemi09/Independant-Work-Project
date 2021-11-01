using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsManager : MonoBehaviour
{
    GameObject weaponHolder;
    Weapon[] weapon = new Weapon[2];
    Weapon currentActiveWeapon = null;
    bool isImmobilized = false;
    
    void Start()
    {
        weaponHolder = GameObject.Find("WeaponHolder");
        for(int i = 0; i < weapon.Length; i ++ )
        {
            weapon[i] = weaponHolder.transform.GetChild(i).GetComponent<Weapon>();
            weapon[i].gameObject.SetActive(false);
        }

        WeaponSlotsCheck();
        // Weapon slots check
    }

    void Update()
    {

    }

    bool CanPlayerShoot()
    {
        if (isImmobilized)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void WeaponSlotsCheck()
    {
        for(int i = 0; i < weapon.Length; i++)
        {
            if (currentActiveWeapon == null)
            {
                currentActiveWeapon = weapon[i];
                currentActiveWeapon.gameObject.SetActive(true);
            }
        }
    }

    void SwitchWeapon()
    {
        GetCurrentWeaponIndex();
    }

    int GetCurrentWeaponIndex()
    {
        for(int i = 0; i < weapon.Length; i++)
        {
            if(currentActiveWeapon == weapon[i])
            {
                return i;
            }
        }
        return -1;
    }
}
