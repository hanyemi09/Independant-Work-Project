using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int m_AmmoPickupAmount;
    void OnTriggerEnter(Collider other)
    {
        PlayerWeaponsController playerWeaponsController = other.GetComponent<PlayerWeaponsController>();
        if (playerWeaponsController != null)
        {
            playerWeaponsController.HandleAddAmmo(m_AmmoPickupAmount);
            PhotonView.Destroy(this.gameObject);
        }
    }
}
