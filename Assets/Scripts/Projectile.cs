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
    [SerializeField] GameObject m_DamagePopup;
    PhotonView m_MyPlayerPhotonView;
    PhotonView m_PhotonView;

    void Start()
    {
        m_Velocity = m_Speed * transform.forward;
        m_PhotonView = GetComponent<PhotonView>();
        Destroy(gameObject, m_LifeTime);
    }
    void FixedUpdate()
    {
        transform.position += m_Velocity * Time.deltaTime; 
    }

    void OnTriggerEnter(Collider other)
    {
        if (!m_PhotonView.IsMine)
            return;

        PhotonView pv = other.gameObject.GetComponent<PhotonView>();
        Health health = other.gameObject.GetComponent<Health>();
        Debug.Log("Hitting");
        if(health && pv)
        {
            pv.RPC("TakeDamage", RpcTarget.All, m_Damage);

        }

        
        PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    void DamagePopupText(Vector3 position)
    {
        PhotonNetwork.Instantiate(m_DamagePopup.name, position, m_DamagePopup.transform.rotation);
    }

    public void SetPhotonView(PhotonView photonView)
    {
        m_MyPlayerPhotonView = photonView;
    }

    public PhotonView GetPhotonView()
    {
        return m_PhotonView;
    }
}
