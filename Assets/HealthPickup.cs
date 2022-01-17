using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HealthPickup : MonoBehaviour
{

    [SerializeField] float m_Health;
    
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
        PhotonView pv = other.GetComponent<PhotonView>();
        Health health = other.GetComponent<Health>();
        if(health && health.GetHealth() < health.GetMaxHealth())
        {
            pv.RPC("AddHealth", RpcTarget.All, m_Health);
            PhotonView.Destroy(this.gameObject);
        }
    }
}
