using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Health : MonoBehaviour
{
    [SerializeField] float m_HitPoints;
    [SerializeField] float m_MaxHitPoints;
    [SerializeField] float m_OverhealPoints;
    [SerializeField] GameObject m_DamagePopup;

    // Start is called before the first frame update
    void Start()
    {
        m_HitPoints = m_MaxHitPoints;
        m_HitPoints = 150f;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_OverhealPoints > 0)
        {
            m_OverhealPoints -= Time.deltaTime * 15f;
        }
    }

    [PunRPC]
    public void TakeDamage(float damage, PhotonView attackerPhotonView, PhotonView receiverPhotonView)
    {
        Debug.Log("Taking Damage");
        PhotonNetwork.Instantiate(m_DamagePopup.name, attackerPhotonView.)
        m_HitPoints -= damage;
    }

    [PunRPC]
    public void AddHealth(float health)
    {
        Debug.Log("Added Health");
        m_HitPoints += health;
        if (m_HitPoints > m_MaxHitPoints)
        {
            m_OverhealPoints = m_HitPoints - m_MaxHitPoints;
            m_HitPoints -= m_OverhealPoints;
        }
    }

    public float GetHealth()
    {
        return m_HitPoints;
    }

    public float GetMaxHealth()
    {
        return m_MaxHitPoints;
    }
}
