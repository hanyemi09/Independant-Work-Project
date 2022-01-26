using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHUDManager : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerWeaponsController m_PlayerWeaponsController;
    Text m_CurrentAmmo;
    Text m_TotalAmmo;
    void Start()
    {
        m_CurrentAmmo = gameObject.transform.GetChild(1).GetComponent<Text>();
        m_TotalAmmo = gameObject.transform.GetChild(2).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PlayerWeaponsController == null)
            return;

        if(m_PlayerWeaponsController.GetCurrentActiveWeapon() == null)
        {
            m_CurrentAmmo.gameObject.SetActive(false);
            m_TotalAmmo.gameObject.SetActive(false);
        }
        else
        {
            m_CurrentAmmo.gameObject.SetActive(true);
            m_TotalAmmo.gameObject.SetActive(true);
        }
    }

    public void Initialize(GameObject player)
    {
        m_PlayerWeaponsController = player.GetComponent<PlayerWeaponsController>();
    }

    public void UpdateCurrentAmmo(WeaponController weapon)
    {
        m_CurrentAmmo.text = weapon.GetCurrentClipAmmo().ToString();
    }

    public void UpdateTotalAmmo(WeaponController weapon)
    {
        m_TotalAmmo.text = "/" + weapon.GetTotalAmmo().ToString();
    }

    public void OnWeaponSwitched(WeaponController weapon)
    {
        if(weapon == null)
        {
            m_CurrentAmmo.gameObject.SetActive(false);
            m_TotalAmmo.gameObject.SetActive(false);
        }
        else if(weapon != null)
        {
            m_CurrentAmmo.text = weapon.GetCurrentClipAmmo().ToString();
            m_TotalAmmo.text = "/ " + weapon.GetTotalAmmo().ToString();
            m_CurrentAmmo.gameObject.SetActive(true);
            m_TotalAmmo.gameObject.SetActive(true);
        }

    }
}
