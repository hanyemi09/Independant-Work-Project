using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class Projectile : MonoBehaviour
{

    Vector3 m_Velocity;
    [SerializeField] float m_Speed;
    [SerializeField] float m_LifeTime;
    [SerializeField] float m_Damage;
    void Start()
    {
        m_Velocity = m_Speed * transform.forward;
        Destroy(gameObject, m_LifeTime);
    }

    void FixedUpdate()
    {
        transform.position += m_Velocity * Time.deltaTime; 
    }

    void OnTriggerEnter(Collider other)
    {
        PhotonView pv = other.gameObject.GetComponent<PhotonView>();
        Health health = other.gameObject.GetComponent<Health>();
        Debug.Log("Hitting");
        if(health && pv)
        {
            pv.RPC("TakeDamage", RpcTarget.MasterClient, m_Damage);
        }
        PhotonNetwork.Destroy(gameObject);
    }
}
