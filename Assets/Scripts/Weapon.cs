using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponShootType
{
    Burst,
    Automatic,
    Manual,
    Charge,
    Throw,
}

public class Weapon : MonoBehaviour 
{
    //GameObject shootVFX;
    // ShootSFX
    [SerializeField] Projectile bulletPrefab;
    Transform shootPoint;

    [SerializeField] WeaponShootType weaponShootType;

    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileSpread;

    [SerializeField] float weaponDamage;
    [SerializeField] float weaponFireRate;
    [SerializeField] float weaponReloadSpeed;
    [SerializeField] float weaponMaxAmmoPerClip;
    [SerializeField] float weaponCurrentAmmo;

    float timeSinceLastShot = 0f;

    void Start()
    {
        shootPoint = GameObject.Find("ShootPoint").transform;
        bulletPrefab.SetProjectileValues(weaponDamage, projectileSpeed);
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    public void HandleShoot(Vector3 dir)
    {
        switch(weaponShootType)
        {
            case WeaponShootType.Automatic:
                TryShoot(dir);
                break;
            case WeaponShootType.Burst:
                TryBurstShoot(dir);
                break;

        }
    }

    void TryShoot(Vector3 shootDir)
    {
        if(timeSinceLastShot > weaponFireRate)
        {
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            weaponCurrentAmmo--;
            timeSinceLastShot = 0f;
        }
    }

    void TryBurstShoot(Vector3 shootdir)
    {

    }

    void HandleReload()
    {

    }

    void TryReload()
    {

    }
}
