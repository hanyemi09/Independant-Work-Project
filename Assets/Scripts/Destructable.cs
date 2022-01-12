using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Destructable : MonoBehaviour
{

    PhotonView photonView;
    PowerupsSpawn powerupSpawn;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        powerupSpawn = GameObject.Find("EventSystem").GetComponent<PowerupsSpawn>();
    }

    void OnTriggerEnter(Collider col)
    {
        powerupSpawn.GetComponent<PhotonView>().RPC("SpawnPowerup", RpcTarget.MasterClient, transform);
        GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.MasterClient);
    }

    [PunRPC]
    void DestroyObject()
    {
        if (this.photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
