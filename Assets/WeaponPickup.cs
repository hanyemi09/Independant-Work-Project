using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponPickup : Pickup
{

    public WeaponController WeaponPrefab;
    // VFX
    // SFX 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        PlayerWeaponsController playerWeaponsController = other.GetComponent<PlayerWeaponsController>();
        if(playerWeaponsController != null)
        {
            playerWeaponsController.CanPickupWeapon(true, this);
        }

    }

    void OnTriggerExit(Collider other)
    {
        PlayerWeaponsController playerWeaponsController = other.GetComponent<PlayerWeaponsController>();
        if (playerWeaponsController != null)
        {
            playerWeaponsController.CanPickupWeapon(false, null);
        }
    }

    public WeaponController GetWeaponPrefab()
    {
        return WeaponPrefab;
    }

}
