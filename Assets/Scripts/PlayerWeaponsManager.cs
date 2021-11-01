using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsManager : MonoBehaviour
{
    GameObject weaponHolder;
    Weapon[] weapon = new Weapon[2];
    Weapon currentActiveWeapon = null;
    bool isImmobilized = false;

    float timeToNextAttack = 0f;
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
        //Debug.Log(currentActiveWeapon);
        timeToNextAttack += Time.deltaTime;
        //Debug.Log(timeToNextAttack);
    }

    public void HandleShoot()
    {
        if(currentActiveWeapon != null && CanPlayerShoot())
        {
            if (timeToNextAttack > currentActiveWeapon.weaponFireRate)
            {
                timeToNextAttack = 0f;
                currentActiveWeapon.WeaponShoot();
            }
        }

    }

    bool CanPlayerShoot()
    {
        if (isImmobilized)
        {
            return false;
        }
        else
        {
            return true;
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

    public void SwitchWeapon()
    {
        int index = GetCurrentWeaponIndex();

        for(int i = 0; i < weapon.Length; i++)
        {
            if(i != index && weapon[i] != null)
            {
                if(currentActiveWeapon != null)
                {
                    currentActiveWeapon.gameObject.SetActive(false);
                }
                currentActiveWeapon = weapon[i];
                currentActiveWeapon.gameObject.SetActive(true);
                timeToNextAttack = 0f;
            }
        }
    }

    Weapon GetCurrentActiveWeapon()
    {
        return currentActiveWeapon;
    }

    int GetCurrentWeaponIndex()
    {
        for(int i = 0; i < weapon.Length; i++)
        {
            if(GetCurrentActiveWeapon() == weapon[i])
            {
                return i;
            }
        }
        return -1;
    }

    void AddWeapon(Weapon Weap)
    {

    }

    public void RemoveCurrentActiveWeapon()
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            if (currentActiveWeapon == weapon[i] && currentActiveWeapon != null)
            {
                Destroy(weapon[i].gameObject);
                currentActiveWeapon = null;
                break;
            }
        }
    }
    public void RemoveWeapon(Weapon Weap)
    {
        for(int i = 0; i < weapon.Length;i++)
        {
            if(Weap == weapon[i])
            {
                Destroy(weapon[i].gameObject);
                currentActiveWeapon = null;
                break;
            }    
        }
    }

    public void RemoveWeaponByIndex(int index)
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            if (index == i && weapon[i] != null)
            {
                Destroy(weapon[i].gameObject);
                currentActiveWeapon = null;
                break;
            }
        }
    }


}
