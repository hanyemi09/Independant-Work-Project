using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Rigidbody rb;
    [SerializeField] bool canRicochet;
    [SerializeField] int timesToRicochet;
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

        Destroy(gameObject, bulletLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Block"))
        {
            if (canRicochet && timesToRicochet > 0)
            {
                Vector3 wallNormal = col.contacts[0].normal;
                Vector3 dir = Vector3.Reflect(rb.velocity, wallNormal).normalized;
                rb.velocity = dir * 2;
                timesToRicochet--;

            }

            if (timesToRicochet <= 0)
            {
                Destroy(gameObject);
            }
            
        }

        if (!col.gameObject.CompareTag("Block") && !col.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);

            ObjectStatsManager colStats = col.gameObject.GetComponent<ObjectStatsManager>();
            if (colStats != null)
            {
                colStats.TakeDamage(bulletDamage);
            }
        }
    }
}
