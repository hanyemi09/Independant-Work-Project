using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Pickup : MonoBehaviour
{

    public GameObject PickupVFX;
    public AudioClip PickupSFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerWeaponsController playerWeaponsController = other.GetComponent<PlayerWeaponsController>();
        if(playerWeaponsController != null)
        {
        }
    }

    public void PlayPickupFeedback()
    {
        
    }

    [PunRPC]
    public void DestroyPickup()
    {
        PhotonNetwork.Destroy(this.gameObject);
    }
}
