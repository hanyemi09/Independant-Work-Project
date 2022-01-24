using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerWeaponsController : MonoBehaviour
{

    WeaponController m_CurrentActiveWeapon;
    bool m_CanPickup;
    Button m_WeaponPickupButton;
    WeaponPickup m_CurrentWeaponPickup;
    GameObject m_WeaponHolder;
    GameObject m_ShootPoint;
    PhotonView m_PhotonView;
    PlayerList m_PlayerList;
    WeaponHUDManager m_WeaponHUDManager;
    WeaponController.WeaponType m_CurrentActiveWeaponType;
    Canvas UICanvas;
    // Start is called before the first frame update
    void Start()
    {
        m_PhotonView = GetComponent<PhotonView>();
        UICanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        m_WeaponHUDManager = GameObject.Find("WeaponHUD").GetComponent<WeaponHUDManager>();

        if (m_WeaponHUDManager != null)
            m_WeaponHUDManager.Initialize(this.gameObject);

        m_WeaponPickupButton = GameObject.Find("WeaponPickup").GetComponent<Button>();
        m_WeaponPickupButton.onClick.AddListener(PickupWeapon);
        m_ShootPoint = gameObject.transform.GetChild(0).gameObject;
        m_WeaponHolder = gameObject.transform.GetChild(2).gameObject;
        m_PlayerList = GameObject.Find("EventSystem").GetComponent<PlayerList>();
    }

    public void Initialize()
    {
        m_WeaponPickupButton = GameObject.Find("WeaponPickup").GetComponent<Button>();
        m_WeaponPickupButton.onClick.AddListener(PickupWeapon);
        m_ShootPoint = gameObject.transform.GetChild(0).gameObject;
        m_WeaponHolder = gameObject.transform.GetChild(2).gameObject;
        m_PlayerList = GameObject.Find("EventSystem").GetComponent<PlayerList>();
    }
    // Update is called once per frame
    void Update()
    {
        // Weapon bobbing
    }

    public void DisableUI()
    {
        for(int i = 0; i < UICanvas.transform.childCount; i++)
        {
            UICanvas.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    [PunRPC]
    public void AddWeapon(string weaponToAdd)
    {
        if (m_CurrentActiveWeapon != null)
        {
            DropWeapon(m_CurrentActiveWeapon);
        }
        m_CurrentActiveWeapon = PhotonNetwork.Instantiate(weaponToAdd, m_WeaponHolder.transform.position, Quaternion.identity).GetComponent<WeaponController>();
        m_CurrentActiveWeaponType = m_CurrentActiveWeapon.GetWeaponType();
        m_WeaponPickupButton.gameObject.SetActive(false);
        m_CurrentActiveWeapon.SetPhotonView(m_PhotonView);
        m_CurrentActiveWeapon.Initialize();
    }

    [PunRPC]
    public void SetWeaponParent(int weaponId, int playerId)
    {
        PhotonView.Find(weaponId).transform.parent = PhotonView.Find(playerId).transform.GetChild(2);
    }

    public void DropWeapon(WeaponController weaponToDrop)
    {
        WeaponController weap = weaponToDrop;
        weaponToDrop.SpawnWeaponPickup(transform);
        Destroy(m_CurrentActiveWeapon.gameObject);
        m_CurrentActiveWeapon = null;
        m_CurrentActiveWeaponType = WeaponController.WeaponType.NONE;
        m_WeaponHUDManager.OnWeaponSwitched(m_CurrentActiveWeapon);
    }

    public void CanPickupWeapon(bool flag, WeaponPickup weaponPickup)
    {
        m_CanPickup = flag;
        m_WeaponPickupButton.gameObject.SetActive(flag);
        m_CurrentWeaponPickup = weaponPickup;
    }

    public void PickupWeapon()
    {
        if (!m_PhotonView.IsMine)
            return;

        Debug.Log("Picking up weapon");
        PhotonView pv = m_CurrentWeaponPickup.GetComponent<PhotonView>();
        if(pv != null)
        {
            pv.RPC("DestroyPickup", RpcTarget.All);
        }
        if(m_PhotonView != null)
        {
            string weaponName = m_CurrentWeaponPickup.GetWeaponPrefab().name;
            AddWeapon(weaponName);
            m_PhotonView.RPC("SetWeaponParent", RpcTarget.All, m_CurrentActiveWeapon.GetPhotonView().ViewID, m_PhotonView.ViewID);
            m_WeaponHUDManager.OnWeaponSwitched(m_CurrentActiveWeapon);
        }
        CanPickupWeapon(false, null);
    }

    public void HandleShoot(Vector3 direction)
    {
        Debug.Log("Handling Shoot");
        if (m_CurrentActiveWeapon == null)
            return;

        //PhotonView pv = m_CurrentActiveWeapon.gameObject.GetComponent<PhotonView>();
        //pv.RPC("TryShoot", RpcTarget.All, direction, m_ShootPoint.transform.position);
        m_CurrentActiveWeapon.TryShoot(direction, m_ShootPoint.transform);
        m_WeaponHUDManager.UpdateCurrentAmmo(m_CurrentActiveWeapon);
    }

    public void HandleAddAmmo(int ammoAmount)
    {
        if(m_CurrentActiveWeapon != null && m_CurrentActiveWeaponType == WeaponController.WeaponType.RANGED)
        {
            m_CurrentActiveWeapon.TryAddAmmo(ammoAmount);
            m_WeaponHUDManager.UpdateTotalAmmo(m_CurrentActiveWeapon);
        }
    }

    public WeaponController GetCurrentActiveWeapon()
    {
        return m_CurrentActiveWeapon;
    }
}
