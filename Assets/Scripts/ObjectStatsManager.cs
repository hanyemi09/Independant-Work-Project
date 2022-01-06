using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class ObjectStatsManager : MonoBehaviour
{
    // sfx
    // vfx
    // death anim
    PhotonView photonView;
    RespawnPlayer respawnPlayer;
    [SerializeField] float objectHealth = 100f;
    [SerializeField] float objectMaxHealth = 100f;
    [SerializeField] Transform damagePopup;
    public Slider slider;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        respawnPlayer = GetComponent<RespawnPlayer>();
    }

    void Update()
    {
        if(slider)
            slider.value = objectHealth;

        if (objectHealth <= 0)
        {
            if (gameObject.layer == 3)
            {
                if(gameObject.activeSelf)
                {
                    photonView.RPC("ObjectStatus", RpcTarget.All, false, this.photonView.ViewID);
                }
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
        DamagePopUp(damage);

    }

    void DamagePopUp(float damage)
    {
        Transform go1 = Instantiate(damagePopup, gameObject.transform.position, damagePopup.rotation);
        TextMeshPro tmp1 = go1.GetComponent<TextMeshPro>();
        tmp1.text = damage.ToString();
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

    [PunRPC]
    public void HealPlayer(float heal)
    {
        if(objectHealth < objectMaxHealth)
            objectHealth += heal;

        if (objectHealth > objectMaxHealth)
            objectHealth = objectMaxHealth;
    }
}   
