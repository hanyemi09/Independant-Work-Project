using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class Projectile : MonoBehaviour
{

    enum ProjectileType
    {
        BULLET,
        THROWABLE
    }
    [SerializeField] ProjectileType projectileType;
    [SerializeField] float bulletDamage = 20f;
    [SerializeField] float bulletLifetime = 3f;
    [SerializeField] float bulletSpeed = 200f;
    [SerializeField] bool canRicochet;
    [SerializeField] int timesToRicochet;
    [SerializeField] Transform damagePopup;
    Rigidbody rb;
    PhotonView photonView;

    // sfx
    // vfx

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(rb != null && projectileType == ProjectileType.BULLET)
        {
            rb.velocity = transform.forward * bulletSpeed;
        }
        else if (rb != null && projectileType == ProjectileType.THROWABLE)
        {

        }
        photonView = GetComponent<PhotonView>();
        Debug.Log(bulletDamage);
    }

    [PunRPC]
    void DestroyProjectile()
    {
        if(this.photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
            Debug.Log("Destroying" + gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bulletLifetime -= Time.deltaTime;

        if (bulletLifetime <= 0f)
            GetComponent<PhotonView>().RPC("DestroyProjectile", RpcTarget.MasterClient);
    }
    
    public void SetProjectileValues(float bulletDmg, float bulletSpd)
    {
        bulletDamage = bulletDmg;
        bulletSpeed = bulletSpd;
    }
    
    public float GetProjectileSpeed()
    {
        return bulletSpeed;
    }

    void OnTriggerEnter(Collider col)
    {
        PhotonView pv = col.gameObject.GetComponent<PhotonView>();
        if (pv != null)
        {
            pv.RPC("TakeDamage", RpcTarget.AllBuffered, bulletDamage);
            GetComponent<PhotonView>().RPC("DestroyProjectile", RpcTarget.AllBuffered);

        }
        else
        {
            GetComponent<PhotonView>().RPC("DestroyProjectile", RpcTarget.AllBuffered);
        }

    }

}
