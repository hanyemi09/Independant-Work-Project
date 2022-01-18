using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HealthPickup : Pickup
{

    [SerializeField] float m_Health;
    PhotonView m_PhotonView;

    // Start is called before the first frame update
    void Start()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        PhotonView pv = other.GetComponent<PhotonView>();
        Health health = other.GetComponent<Health>();
        if(health && health.GetHealth() < health.GetMaxHealth())
        {
            pv.RPC("AddHealth", RpcTarget.All, m_Health);
            m_PhotonView.RPC("DestroyPickup", RpcTarget.All);
        }
    }
}
