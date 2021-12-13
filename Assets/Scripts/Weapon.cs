using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public enum WeaponShootType
{
    Burst,
    Automatic,
    Manual,
    Charge,
    Throw,
    Shotgun,
    Melee,
}

public class Weapon : MonoBehaviour
{
    //GameObject shootVFX;
    // ShootSFX
    [SerializeField] Projectile bulletPrefab;
    [SerializeField] Rigidbody grenadePrefab;
    [SerializeField] Animator anim;
    [SerializeField] Transform damagePopup;
    Transform shootPoint;
    Transform throwPoint;

    [SerializeField] WeaponShootType weaponShootType;

    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileSpread;

    [SerializeField] float shootSpread = 15f;
    [SerializeField] float pelletsPerShot;
    [SerializeField] float weaponDamage = 20f;
    [SerializeField] float weaponDamageAmount;
    [SerializeField] float weaponDamageMultiplier = 1f;
    [SerializeField] float weaponFireRate;
    [SerializeField] float weaponFireRateAmount;
    [SerializeField] float weaponReloadSpeed;
    [SerializeField] float weaponAmmoReload;
    [SerializeField] float weaponMaxAmmoPerClip;
    [SerializeField] float weaponCurrentAmmo;
    [SerializeField] float meleeAttackRange;
    [SerializeField] LayerMask whatIsEnemies;
    float distanceMultiplier = 4f;
    float timeSinceLastShot = 0f;
    float timeSinceLastReload = 0f;
    bool burstComplete = false;

    void Start()
    {
        weaponDamageAmount = weaponDamage;
        weaponFireRateAmount = weaponFireRate;
        timeSinceLastShot = weaponFireRate;
        shootPoint = GameObject.Find("ShootPoint").transform;
        throwPoint = GameObject.Find("ThrowPoint").transform;
        if(bulletPrefab != null)
        {
            bulletPrefab.SetProjectileValues(weaponDamageAmount, projectileSpeed);
        }
        anim = GetComponent<Animator>();
        GetComponent<Collider>().enabled = false;

    }

    void Update()
    {
        weaponDamageAmount = weaponDamage * weaponDamageMultiplier;

        timeSinceLastShot += Time.deltaTime;
        timeSinceLastReload += Time.deltaTime;

        if(timeSinceLastShot > weaponReloadSpeed && timeSinceLastReload > weaponReloadSpeed)
        {
            timeSinceLastReload = 0f;
            weaponCurrentAmmo += weaponAmmoReload;

            if(weaponCurrentAmmo > weaponMaxAmmoPerClip)
            {
                weaponCurrentAmmo = weaponMaxAmmoPerClip;
            }
        }
    }

    public void HandleShoot(Vector3 dir)
    {

        if (timeSinceLastShot > weaponFireRateAmount)
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
                case WeaponShootType.Melee:
                    TryMelee();
                    //pv.RPC("TryMelee", RpcTarget.AllBuffered);
                    break;
            }
        }   
    }

    void TryMelee()
    {
        if(anim != null)
        {
            timeSinceLastShot = 0f;
            GetComponent<Collider>().enabled = true;
            anim.SetTrigger("attack");

            StartCoroutine(playAndWaitForAnim());
        }
    }

    IEnumerator playAndWaitForAnim()
    {
        
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        GetComponent<Collider>().enabled = false;
        //Done playing. Do something below!
        Debug.Log("Done Playing");
    }

    void OnTriggerEnter(Collider col)
    {
        PhotonView pv = col.gameObject.GetComponent<PhotonView>();
        if (pv != null)
        {
            Debug.Log("Hit Enemy");
            pv.RPC("TakeDamage", RpcTarget.AllBuffered, weaponDamage);
            Transform go = Instantiate(damagePopup, gameObject.transform.position, damagePopup.rotation);
            TextMeshPro tmp = go.GetComponent<TextMeshPro>();
            tmp.text = weaponDamage.ToString();
        }
        //else if(pv == null)
        //{
        //    Transform go1 = Instantiate(damagePopup, gameObject.transform.position, damagePopup.rotation);
        //    TextMeshPro tmp1 = go1.GetComponent<TextMeshPro>();
        //    tmp1.text = weaponDamage.ToString();
        //    col.
        //}
    }

    void TryShoot(Vector3 shootDir, float pellets)
    {

        for(int i = 0; i < pellets;i++)
        {
            float spread = Random.Range(-shootSpread, shootSpread);
            Quaternion rot = shootPoint.rotation;
            //rot.y += spread;
            //Instantiate(bulletPrefab, shootPoint.position, rot);
            PhotonNetwork.Instantiate(bulletPrefab.name, shootPoint.position, rot);
            weaponCurrentAmmo--;
            timeSinceLastShot = 0f;
        }
    }

    IEnumerator TryBurstShoot(Vector3 shootDir)
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

    void TryThrowGrenade(Vector3 shootDir)
    {
        if (shootDir.magnitude > 0.155f)
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

    [PunRPC]
    public void DamageBuff(float dmgMultiplier, float duration)
    {
        weaponDamageMultiplier = dmgMultiplier;
        weaponDamageAmount = weaponDamage * weaponDamageMultiplier;
        bulletPrefab.SetProjectileValues(weaponDamageAmount, projectileSpeed);
        StartCoroutine(DamageBuffDuration(duration));
    }

    IEnumerator DamageBuffDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        weaponDamageMultiplier = 1f;
        weaponDamageAmount = weaponDamage * weaponDamageMultiplier;
        bulletPrefab.SetProjectileValues(weaponDamageAmount, projectileSpeed);
    }

    public void AtkSpdBuff(float atkSpd, float duration)
    {
        weaponFireRateAmount = weaponFireRate / atkSpd;
        StartCoroutine(AtkSpdBuffDuration(duration));
    }

    IEnumerator AtkSpdBuffDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        weaponFireRateAmount = weaponFireRate;
    }


}
