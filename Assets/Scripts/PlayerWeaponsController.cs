using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerWeaponsController : MonoBehaviour
{

    WeaponController[] m_WeaponSlots = new WeaponController[2];
    bool m_CanPickup;
    Button m_WeaponPickupButton;
    Button m_WeaponSwapButton;
    WeaponPickup m_CurrentWeaponPickup;
    GameObject m_WeaponHolder;
    GameObject m_ShootPoint;
    PhotonView m_PhotonView;
    PlayerList m_PlayerList;
    WeaponHUDManager m_WeaponHUDManager;
    WeaponController.WeaponType m_CurrentActiveWeaponType;
    int m_CurrentActiveWeaponIndex = -1;
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
        m_WeaponSlots[0] = m_WeaponHolder.transform.GetChild(0).gameObject.GetComponent<WeaponController>();
        m_WeaponSwapButton = GameObject.Find("WeaponSwap").GetComponent<Button>();
        m_WeaponSwapButton.onClick.AddListener(SwitchWeapon);
        if(m_WeaponSlots[0] != null)
        {
            m_CurrentActiveWeaponIndex = 0;
            m_CurrentActiveWeaponType = m_WeaponSlots[m_CurrentActiveWeaponIndex].GetWeaponType();
        }
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
            GameObject temp = UICanvas.transform.GetChild(i).gameObject;
            if (temp.tag != "Spectator")
            {
                temp.SetActive(false);
            }
            else
            {
                temp.SetActive(true);
            }
        }
    }

    [PunRPC]
    public void AddWeapon(string weaponToAdd)
    {
        if (m_WeaponSlots[1] != null)
        {
            DropWeapon(m_WeaponSlots[1]);
        }
        m_PhotonView.RPC("DisableCurrentActiveWeapon", RpcTarget.All);
        m_WeaponSlots[1] = PhotonNetwork.Instantiate(weaponToAdd, m_WeaponHolder.transform.position, Quaternion.identity).GetComponent<WeaponController>();
        m_WeaponSlots[1].SetPhotonView(m_PhotonView);
        m_WeaponSlots[1].Initialize();
        m_WeaponPickupButton.gameObject.SetActive(false);
        m_PhotonView.RPC("DisableWeapon", RpcTarget.All, 1);

    }
    [PunRPC]
    public void DisableWeapon(int index)
    {
        if(m_WeaponSlots[index] != null)
        {
            m_WeaponSlots[index].gameObject.SetActive(false);
        }
    }

    [PunRPC]
    public void SwitchWeapon()
    {
        m_WeaponSlots[m_CurrentActiveWeaponIndex].gameObject.SetActive(false);
        m_CurrentActiveWeaponType = WeaponController.WeaponType.NONE;
        int tempIndex = m_CurrentActiveWeaponIndex;
        m_CurrentActiveWeaponIndex = -1;
        if (tempIndex == 0)
        {
            tempIndex = 1;
        }
        else
        {
            tempIndex = 0;
        }
        m_WeaponSlots[tempIndex].gameObject.SetActive(true);
        m_CurrentActiveWeaponIndex = tempIndex;
        m_CurrentActiveWeaponType = m_WeaponSlots[tempIndex].GetWeaponType();
    }

    [PunRPC]
    public void DisableCurrentActiveWeapon()
    {
        if(m_CurrentActiveWeaponIndex != 0)
        {
            m_WeaponSlots[m_CurrentActiveWeaponIndex].gameObject.SetActive(false);
            m_CurrentActiveWeaponIndex = -1;
            m_CurrentActiveWeaponType = WeaponController.WeaponType.NONE;
        }
        SwitchToWeapon(0);
    }

    [PunRPC]
    public void SwitchToWeapon(int index)
    {
        m_CurrentActiveWeaponIndex = index;
        m_WeaponSlots[m_CurrentActiveWeaponIndex].gameObject.SetActive(true);
        m_CurrentActiveWeaponType = m_WeaponSlots[m_CurrentActiveWeaponIndex].GetWeaponType();
        m_WeaponHUDManager.OnWeaponSwitched(m_WeaponSlots[m_CurrentActiveWeaponIndex]);

    }

    [PunRPC]
    public void SetWeaponParent(int weaponId, int playerId)
    {
        PhotonView.Find(weaponId).transform.parent = PhotonView.Find(playerId).transform.GetChild(2);
    }

    public void DropWeapon(WeaponController weaponToDrop)
    {
        if(weaponToDrop == m_WeaponSlots[1])
        {
            WeaponController weap = weaponToDrop;
            weaponToDrop.SpawnWeaponPickup(transform);
            Destroy(m_WeaponSlots[1].gameObject);
            m_CurrentActiveWeaponIndex = 0;
            m_WeaponSlots[1] = null;
            m_CurrentActiveWeaponType = WeaponController.WeaponType.NONE;
            m_WeaponHUDManager.OnWeaponSwitched(m_WeaponSlots[1]);
        }
        m_PhotonView.RPC("SwitchToWeapon", RpcTarget.All, 0);
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
        if (m_CurrentWeaponPickup == null)
            return;

        PhotonView pv = m_CurrentWeaponPickup.GetComponent<PhotonView>();
        if(pv != null)
        {
            pv.RPC("DestroyPickup", RpcTarget.All);
        }
        if(m_PhotonView != null)
        {
            string weaponName = m_CurrentWeaponPickup.GetWeaponPrefab().name;
            AddWeapon(weaponName);
            m_PhotonView.RPC("SetWeaponParent", RpcTarget.All, m_WeaponSlots[1].GetPhotonView().ViewID, m_PhotonView.ViewID);
            m_WeaponHUDManager.OnWeaponSwitched(m_WeaponSlots[1]);
        }
        CanPickupWeapon(false, null);
    }

    public void HandleShoot(Vector3 direction)
    {
        Debug.Log("Handling Shoot");
        if (m_WeaponSlots == null)
            return;

        //PhotonView pv = m_CurrentActiveWeapon.gameObject.GetComponent<PhotonView>();
        //pv.RPC("TryShoot", RpcTarget.All, direction, m_ShootPoint.transform.position);
        m_WeaponSlots[m_CurrentActiveWeaponIndex].TryShoot(direction, m_ShootPoint.transform);
        m_WeaponHUDManager.UpdateCurrentAmmo(m_WeaponSlots[m_CurrentActiveWeaponIndex]);
    }

    public void HandleAddAmmo(int ammoAmount)
    {
        if(m_WeaponSlots != null && m_CurrentActiveWeaponType == WeaponController.WeaponType.RANGED)
        {
            m_WeaponSlots[m_CurrentActiveWeaponIndex].TryAddAmmo(ammoAmount);
            m_WeaponHUDManager.UpdateTotalAmmo(m_WeaponSlots[m_CurrentActiveWeaponIndex]);
        }
    }

    public WeaponController GetCurrentActiveWeapon()
    {
        return m_WeaponSlots[m_CurrentActiveWeaponIndex];
    }
}
