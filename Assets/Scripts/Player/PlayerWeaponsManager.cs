using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsManager : MonoBehaviour
{
    Weapon[] weapons = new Weapon[1];
    Weapon currentActiveWeapon;
    [SerializeField] GameObject weaponHolder;
    
    void Start()
    {

        weaponHolder = GameObject.Find("WeaponHolder");
        // Initialize the weapons here
        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = weaponHolder.transform.GetChild(i).GetComponent<Weapon>();
            weapons[i].gameObject.SetActive(false);
        }

        // Set the first weapon to be the player's active weapon
        for (int i = 0; i < weapons.Length; i++)
        {
            if(currentActiveWeapon == null)
            {
                weapons[i].gameObject.SetActive(true);
                currentActiveWeapon = weapons[i];
            }
        }
    }
    
    void Update()
    {

    }

    public void HandleShoot(Vector3 dir)
    {
        // Call the current weapon that is shooting
        currentActiveWeapon.HandleShoot(dir);
    }
}
