using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponController : MonoBehaviour
{
    public  enum WeaponType
    {
        RANGED,
        MELEE,
        TOTALTYPES,
        NONE,
    }

    public enum ReloadType
    {
        SINGLE,
        FULL,
        TOTALTYPES,
    }
    [SerializeField] WeaponType m_WeaponType;
    [SerializeField] ReloadType m_ReloadType;
    [SerializeField] WeaponPickup m_WeaponPickupPrefab;
    [SerializeField] Projectile m_BulletPrefab;
    [SerializeField] float m_WeaponFireRate;
    [SerializeField] int m_AmmoPerClip;
    [SerializeField] float m_HalfReloadTiming;
    [SerializeField] float m_FullReloadTiming;
    [SerializeField] int m_TotalAmmo;
    [SerializeField] Animator m_Animator;
    WeaponHUDManager m_WeaponHUDManager;
    PhotonView m_PhotonView;
    PhotonView m_MyPlayerPhotonView;
    float m_TimeSinceLastFired;
    int m_CurrentClipAmmo;
    bool m_IsAnimationPlaying;
    [SerializeField] float m_ComboAvailableLength;
    bool m_ComboAvailable;
    int m_PunchCombo = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentClipAmmo = m_AmmoPerClip;
        m_PhotonView = GetComponent<PhotonView>();
        m_WeaponHUDManager = GameObject.Find("WeaponHUD").GetComponent<WeaponHUDManager>();
        m_Animator = GetComponent<Animator>();
    }

    public void Initialize()
    {
        m_PhotonView = GetComponent<PhotonView>();
        m_WeaponHUDManager = GameObject.Find("WeaponHUD").GetComponent<WeaponHUDManager>();
    }
    // Update is called once per frame
    void Update()
    {
        m_TimeSinceLastFired += Time.deltaTime;

        if (m_CurrentClipAmmo <= 0)
        {
            TryReload();
            m_WeaponHUDManager.UpdateCurrentAmmo(this);
            m_WeaponHUDManager.UpdateTotalAmmo(this);
        }
    }

    public void SpawnWeaponPickup(Transform transform)
    {
        // Instantiate
        PhotonNetwork.Instantiate(m_WeaponPickupPrefab.name, transform.position, transform.rotation);
    }

    public virtual void TryShoot(Vector3 direction, Transform goPosition)
    {
        if (m_WeaponFireRate > m_TimeSinceLastFired)
            return;

        if(m_WeaponType == WeaponType.RANGED)
        {
            if (m_CurrentClipAmmo > 0)
            {
                Quaternion rot = goPosition.rotation;
                Projectile proj = PhotonNetwork.Instantiate(m_BulletPrefab.name, goPosition.position, rot).gameObject.GetComponent<Projectile>();
                m_CurrentClipAmmo--;
            }

            if (m_CurrentClipAmmo <= 0)
            {
                TryReload();
            }
        }
        else if(m_WeaponType == WeaponType.MELEE)
        {
            if (m_Animator == null)
                return;

            if (m_TimeSinceLastFired > 1f)
            {
                m_PunchCombo = 0;
            }
            m_TimeSinceLastFired = 0f;
            //00GetComponent<Collider>().enabled = true;


            if(m_IsAnimationPlaying == false)
            {
                if (m_PunchCombo == 0)
                {
                    m_Animator.SetTrigger("FirstPunch");
                    m_PunchCombo = 1;
                    StartCoroutine(PlayMeleeAnimation());
                }
                else if (m_PunchCombo == 1)
                {
                    if(m_ComboAvailable)
                    {
                        m_Animator.SetTrigger("SecondPunch");
                        m_PunchCombo = 2;
                        StartCoroutine(PlayMeleeAnimation());
                    }
                }
                else if (m_PunchCombo == 2)
                {
                    if(m_ComboAvailable)
                    {
                        m_Animator.SetTrigger("ThirdPunch");
                        m_PunchCombo = 0;
                        StartCoroutine(PlayMeleeAnimation());
                    }
                }
            }


            //StartCoroutine(PlayMeleeAnimation());
        }
        m_TimeSinceLastFired = 0f;

    }

    IEnumerator PlayMeleeAnimation()
    {
        m_IsAnimationPlaying = true;
        //yield return new WaitForSeconds(m_Animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(0.6f);
        m_IsAnimationPlaying = false;
        m_ComboAvailable = true;
        yield return new WaitForSeconds(m_ComboAvailableLength);
        m_ComboAvailable = false;

    }
    public void TryReload()
    {
        if (m_TotalAmmo <= 0)
            return;

        m_TimeSinceLastFired = 0;

        if(m_ReloadType == ReloadType.FULL)
        {
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
        else if(m_ReloadType == ReloadType.SINGLE)
        {
            if (m_CurrentClipAmmo > 0)
            {

                m_TimeSinceLastFired -= m_HalfReloadTiming;
            }
            else
            {
                m_TimeSinceLastFired -= m_FullReloadTiming;
            }
            int ammoAdded = 1;
            m_CurrentClipAmmo += ammoAdded;
            m_TotalAmmo -= ammoAdded;
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

    public PhotonView GetPhotonView()
    {
        return m_PhotonView;
    }

    public int GetAmmoPerClip()
    {
        return m_AmmoPerClip;
    }

    public WeaponType GetWeaponType()
    {
        return m_WeaponType;
    }
}
