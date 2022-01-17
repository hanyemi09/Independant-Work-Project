using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Pun;
public class PlayerWeaponsController : MonoBehaviour
{

    WeaponController m_CurrentActiveWeapon;
    bool m_CanPickup;
    Button m_WeaponPickupButton;
    WeaponPickup m_CurrentWeaponPickup;
    GameObject m_WeaponHolder;
    GameObject m_ShootPoint;
    PhotonView m_PhotonView;
    // Start is called before the first frame update
    void Start()
    {
        m_WeaponPickupButton = GameObject.Find("WeaponPickup").GetComponent<Button>();
        m_WeaponPickupButton.gameObject.SetActive(false);
        m_WeaponPickupButton.onClick.AddListener(PickupWeapon);
        m_ShootPoint = gameObject.transform.GetChild(0).gameObject;
        m_WeaponHolder = gameObject.transform.GetChild(2).gameObject;
        m_PhotonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        // Weapon bobbing
    }

    [PunRPC]
    public void AddWeapon(string weaponToAdd)
    {
        if(m_CurrentActiveWeapon != null)
        {
            DropWeapon(m_CurrentActiveWeapon);
            m_CurrentActiveWeapon = PhotonNetwork.Instantiate(weaponToAdd, m_WeaponHolder.transform.position, Quaternion.identity).GetComponent<WeaponController>();
            m_CurrentActiveWeapon.transform.parent = m_WeaponHolder.transform;
            m_WeaponPickupButton.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Spawning Weapon");
            m_CurrentActiveWeapon = PhotonNetwork.Instantiate(weaponToAdd, m_WeaponHolder.transform.position, Quaternion.identity).GetComponent<WeaponController>();
            m_CurrentActiveWeapon.transform.parent = m_WeaponHolder.transform;
            m_WeaponPickupButton.gameObject.SetActive(false);
        }
    }

    public void DestroyWeapon(WeaponController weaponToDestroy)
    {
        Destroy(weaponToDestroy.gameObject);
        m_CurrentActiveWeapon = null;
    }

    public void DropWeapon(WeaponController weaponToDrop)
    {
        WeaponController weap = weaponToDrop;
        weaponToDrop.SpawnWeaponPickup(transform);
        Destroy(m_CurrentActiveWeapon.gameObject);
        m_CurrentActiveWeapon = null;
    }

    public WeaponController GetActiveWeapon()
    {
        return m_CurrentActiveWeapon;
    }

    public void CanPickupWeapon(bool flag, WeaponPickup weaponPickup)
    {
        m_CanPickup = flag;
        m_WeaponPickupButton.gameObject.SetActive(flag);
        m_CurrentWeaponPickup = weaponPickup;
    }

    public void PickupWeapon()
    {
        Debug.Log("Picking up weapon");
        PhotonView pv = m_CurrentWeaponPickup.GetComponent<PhotonView>();
        if(pv != null)
        {
            pv.RPC("DestroyPickup", RpcTarget.All);
        }
        if(m_PhotonView != null)
        {
            string weaponName = m_CurrentWeaponPickup.GetWeaponPrefab().name;
            m_PhotonView.RPC("AddWeapon", RpcTarget.All, weaponName);
        }

    }

    public void HandleShoot(Vector3 direction)
    {
        Debug.Log("Handling Shoot");
        if (m_CurrentActiveWeapon == null)
            return;

        PhotonView pv = m_CurrentActiveWeapon.gameObject.GetComponent<PhotonView>();
        pv.RPC("TryShoot", RpcTarget.All, direction, m_ShootPoint.transform.position);
        //m_CurrentActiveWeapon.TryShoot(direction, m_ShootPoint.transform.position);
    }

    public void HandleAddAmmo(int ammoAmount)
    {
        if(m_CurrentActiveWeapon != null)
        {
            m_CurrentActiveWeapon.TryAddAmmo(ammoAmount);
        }
    }
}