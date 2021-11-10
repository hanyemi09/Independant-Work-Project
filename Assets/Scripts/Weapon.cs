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
    Shotgun,
}

public class Weapon : MonoBehaviour
{
    //GameObject shootVFX;
    // ShootSFX
    [SerializeField] Projectile bulletPrefab;
    [SerializeField] Rigidbody grenadePrefab;
    Transform shootPoint;
    Transform throwPoint;

    [SerializeField] WeaponShootType weaponShootType;

    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileSpread;

    [SerializeField] float shootSpread = 15f;
    [SerializeField] float pelletsPerShot;
    [SerializeField] float weaponDamage;
    [SerializeField] float weaponFireRate;
    [SerializeField] float weaponReloadSpeed;
    [SerializeField] float weaponMaxAmmoPerClip;
    [SerializeField] float weaponCurrentAmmo;
    float distanceMultiplier = 4f;
    float timeSinceLastShot = 0f;
    bool burstComplete = false;

    void Start()
    {
        shootPoint = GameObject.Find("ShootPoint").transform;
        throwPoint = GameObject.Find("ThrowPoint").transform;
        bulletPrefab.SetProjectileValues(weaponDamage, projectileSpeed);
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    public void HandleShoot(Vector3 dir)
    {
        switch (weaponShootType)
        {
            case WeaponShootType.Automatic:
                TryShoot(dir, 1);
                break;
            case WeaponShootType.Shotgun:
                TryShoot(dir, pelletsPerShot);
                break;
            case WeaponShootType.Burst:
                StartCoroutine(TryBurstShoot(dir));
                break;
            case WeaponShootType.Throw:
                TryThrowGrenade(dir);
                break;

        }
    }

    void TryShoot(Vector3 shootDir, float pellets)
    {
        if (timeSinceLastShot > weaponFireRate)
        {

            for(int i = 0; i < pellets;i++)
            {
                float spread = Random.Range(-shootSpread, shootSpread);
                Quaternion rot = shootPoint.rotation;
                rot.y += spread;
                Instantiate(bulletPrefab, shootPoint.position, rot);
                weaponCurrentAmmo--;
                timeSinceLastShot = 0f;
            }
        }
    }

    IEnumerator TryBurstShoot(Vector3 shootDir)
    {

        if (timeSinceLastShot > weaponFireRate)
        {
            burstComplete = false;
            if (burstComplete == false)
            {

                timeSinceLastShot = 0f;
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                    weaponCurrentAmmo--;
                    yield return new WaitForSeconds(0.15f);
                }
            }
            burstComplete = true;
        }
    }

    void TryThrowGrenade(Vector3 shootDir)
    {
        if (shootDir.magnitude > 0.155f)
        {

            if (timeSinceLastShot > weaponFireRate)
            {

                Vector3 throwAtPosition = shootDir * distanceMultiplier;
                Vector3 distance = throwAtPosition - shootDir;
                Vector3 distanceXZ = distance;
                float hypo = distance.x * distance.x + distance.z * distance.z;
                float time = hypo / hypo / 2;

                distanceXZ.y = 0f;

                float Sy = distance.y;
                float Sxz = distanceXZ.magnitude;

                float Vxz = Sxz / time;
                float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y);

                Vector3 result = distanceXZ.normalized;
                result *= Vxz;
                result.y = Vy;

                Rigidbody rb = Instantiate(grenadePrefab, throwPoint.position, Quaternion.identity);
                weaponCurrentAmmo--;
                rb.velocity = result;
                timeSinceLastShot = 0f;
            }
        }
    }   

    void HandleReload()
    {

    }

    void TryReload()
    {

    }
}
