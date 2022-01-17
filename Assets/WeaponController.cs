using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponController : MonoBehaviour
{

    [SerializeField] WeaponPickup m_WeaponPickupPrefab;
    [SerializeField] Projectile m_BulletPrefab;
    [SerializeField] float m_WeaponFireRate;
    [SerializeField] int m_AmmoPerClip;
    [SerializeField] float m_HalfReloadTiming;
    [SerializeField] float m_FullReloadTiming;
    [SerializeField] int m_TotalAmmo;

    PhotonView m_MyPlayerPhotonView;
    float m_TimeSinceLastFired;
    int m_CurrentClipAmmo;
    

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentClipAmmo = m_AmmoPerClip;
    }

    // Update is called once per frame
    void Update()
    {
        m_TimeSinceLastFired += Time.deltaTime;
        if(m_CurrentClipAmmo <= 0)
        {
            TryReload();
        }
    }

    public void SpawnWeaponPickup(Transform transform)
    {
        // Instantiate
        Instantiate(m_WeaponPickupPrefab, transform.position, transform.rotation);
    }

    [PunRPC]
    public void TryShoot(Vector3 direction, Vector3 goPosition)
    {
        if (m_WeaponFireRate > m_TimeSinceLastFired)
            return;

        if(m_CurrentClipAmmo > 0)
        {
            Projectile proj = PhotonNetwork.Instantiate(m_BulletPrefab.name,goPosition,Quaternion.identity).gameObject.GetComponent<Projectile>();
            proj.transform.forward = direction;
            proj.SetPhotonView(m_MyPlayerPhotonView);
            m_TimeSinceLastFired = 0f;
            m_CurrentClipAmmo--;
        }
    }

    public void TryReload()
    {
        if (m_TotalAmmo <= 0)
            return;

        m_TimeSinceLastFired = 0;

        if (m_CurrentClipAmmo > 0)
        {
            int ammoAdded = m_AmmoPerClip - m_CurrentClipAmmo;
            m_CurrentClipAmmo += ammoAdded;
            m_TotalAmmo -= ammoAdded;
            m_TimeSinceLastFired -= m_HalfReloadTiming;
        }
        else
        {
            int ammoAdded = m_AmmoPerClip - m_CurrentClipAmmo;
            m_CurrentClipAmmo += ammoAdded;
            m_TotalAmmo -= ammoAdded;
            m_TimeSinceLastFired -= m_FullReloadTiming;
        }
    }

    public void TryAddAmmo(int ammoAmount)
    {
        m_TotalAmmo += ammoAmount;
    }

    public int GetCurrentClipAmmo()
    {
        return m_CurrentClipAmmo;
    }

    public int GetTotalAmmo()
    {
        return m_TotalAmmo;
    }

    public void SetPhotonView(PhotonView photonView)
    {
        m_MyPlayerPhotonView = photonView;
    }
}
