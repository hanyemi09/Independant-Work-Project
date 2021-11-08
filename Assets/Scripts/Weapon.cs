using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour 
{
    private enum WEAPONTYPE {
        MELEE, RANGED
    };

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float weaponFireRate = 0.5f;
    [SerializeField] float weaponDamage = 1f;
    [SerializeField] WEAPONTYPE weaponType;
    [SerializeField] int weaponCurrentAmmo = 30;
    [SerializeField] int weaponReloadAmount = 30;
    [SerializeField] int weaponClipAmmo = 30;
    [SerializeField] int weaponAmmoPerShot = 1;
    [SerializeField] float timeToStartReload = 1.5f;

    Transform shootPoint;

    WEAPONTYPE GetWeaponType()    
    {
        return weaponType;
    }

    void Start()
    {
        shootPoint = GameObject.Find("ShootPoint").GetComponent<Transform>();
        if(bulletPrefab != null)
        {
            Projectile proj = bulletPrefab.GetComponent<Projectile>();
            proj.SetBulletDamage(weaponDamage);
        }
    }

    public void WeaponAttack()
    {
        if(weaponType == WEAPONTYPE.MELEE)
        {

        }
        else if(weaponType == WEAPONTYPE.RANGED)
        {
            WeaponShoot();
        }
    }

    public void WeaponShoot()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        weaponCurrentAmmo -= weaponAmmoPerShot;
    }

    public void WeaponReload()
    {
        if(weaponCurrentAmmo < weaponClipAmmo)
        {
            weaponCurrentAmmo += weaponReloadAmount;
            Mathf.Clamp(weaponCurrentAmmo, 0, weaponClipAmmo);
        }

        if(weaponCurrentAmmo > weaponClipAmmo)
        {
            weaponCurrentAmmo = weaponClipAmmo;
        }
    }

    public int GetCurrentAmmo()
    {
        return weaponCurrentAmmo;
    }

    public float GetWeaponFireRate()
    {
        return weaponFireRate;
    }

    public float GetTimeToStartReloading()
    {
        return timeToStartReload;
    }
}
