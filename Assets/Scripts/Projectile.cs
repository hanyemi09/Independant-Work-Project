using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class Projectile : MonoBehaviour
{

    enum ProjectileType
    {
        BULLET
    }
    [SerializeField] ProjectileType projectileType;
    [SerializeField] float bulletDamage = 20f;
    [SerializeField] float bulletLifetime = 3f;
    [SerializeField] float bulletSpeed = 200f;
    Rigidbody rb;
    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
        rb.velocity = transform.forward * bulletSpeed;
    }



    // Update is called once per frame
    void Update()
    {
        bulletLifetime -= Time.deltaTime;

        if (bulletLifetime <= 0f)
        {
            GetComponent<PhotonView>().RPC("DestroyProjectile", RpcTarget.MasterClient);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        PhotonView pv = col.gameObject.GetComponent<PhotonView>();

        if (pv != null)
        {
            pv.RPC("TakeDamage", RpcTarget.AllBuffered, bulletDamage);
        }

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

    [PunRPC]
    void DestroyProjectile()
    {
        if (this.photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
            Debug.Log("Destroying" + gameObject);
        }
    }
}
