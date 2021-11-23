using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectStatsManager : MonoBehaviour
{
    // sfx
    // vfx
    // death anim
    PhotonView photonView;
    RespawnPlayer respawnPlayer;
    [SerializeField] float objectHealth = 100f;
    bool isInvincible = false;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        respawnPlayer = GetComponent<RespawnPlayer>();
    }

    void Update()
    {
        if (objectHealth <= 0)
        {
            if (gameObject.layer == 3)
            {
                photonView.RPC("ObjectStatus", RpcTarget.All, false, this.photonView.ViewID);
            }
            else
            {
                photonView.RPC("DestroyObject", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        objectHealth -= damage;
    }

    [PunRPC]
    void DestroyObject()
    {
        if (photonView.IsMine)
            PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    void ObjectStatus(bool tf, int ID)
    {
        if (!tf)
        {
            PhotonView.Find(ID).gameObject.SetActive(tf);
        }
        else if (tf)
        {
            PhotonView.Find(ID).gameObject.SetActive(tf);
            objectHealth = 100f;
        }
    }

    public void RespawnDaPlayer()
    {
        photonView.RPC("ObjectStatus", RpcTarget.All, true, this.photonView.ViewID);
    }
}   
