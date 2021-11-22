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

    float objectHealth = 100f;
    bool isInvincible = false;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (objectHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        if (!photonView.IsMine)
            return;

        objectHealth -= damage;
    }


}
