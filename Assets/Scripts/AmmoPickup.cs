using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AmmoPickup : Pickup
{
    [SerializeField] int m_AmmoPickupAmount;

    void Start()
    {
        base.Start();
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerWeaponsController playerWeaponsController = other.GetComponent<PlayerWeaponsController>();
        if (playerWeaponsController != null)
        {
            playerWeaponsController.HandleAddAmmo(m_AmmoPickupAmount);
            m_PhotonView.RPC("DestroyPickup", RpcTarget.All);
        }
    }
}
