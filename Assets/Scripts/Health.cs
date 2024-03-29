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
    PhotonView m_PhotonView;
    PlayerList m_PlayerList;
    // Start is called before the first frame update
    void Start()
    {
        m_HitPoints = m_MaxHitPoints;
        m_HitPoints = 150f;
        m_PhotonView = GetComponent<PhotonView>();
        m_PlayerList = GameObject.Find("EventSystem").GetComponent<PlayerList>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_OverhealPoints > 0)
        {
            m_OverhealPoints -= Time.deltaTime * 15f;
        }

        if(m_HitPoints <= 0 && gameObject.layer != 3 && gameObject.layer != 13)
        {
            m_PhotonView.RPC("DestroyObject", RpcTarget.AllBuffered);
        }
        else if(m_HitPoints <= 0 && gameObject.layer != 3 && gameObject.layer == 13)
        {
            GetComponent<PowerupsSpawn>();
            m_PhotonView.RPC("SpawnPowerup", RpcTarget.All);
        }
        else if(m_HitPoints <= 0 && gameObject.layer == 3 && gameObject.layer != 13)
        {
            PlayerWeaponsController playerWeaponsController = GetComponent<PlayerWeaponsController>();
            playerWeaponsController.DisableUI();
            CameraMotor camera = GameObject.Find("Main Camera").GetComponent<CameraMotor>();
            camera.SwitchCamera(false);
            m_PhotonView.RPC("DestroyObject", RpcTarget.AllBuffered);

        }
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        Debug.Log("Taking Damage");
        if (m_OverhealPoints > 0)
            m_OverhealPoints -= damage * 2f;
        else
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

    [PunRPC]
    void DestroyObject()
    {
        m_PlayerList.RemoveFromList(this.gameObject);
        PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
