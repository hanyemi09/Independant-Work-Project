using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float bulletDamage = 20f;
    [SerializeField] float bulletLifetime = 3f;
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody rb;
    // sfx
    // vfx

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
        Destroy(gameObject, bulletLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetBulletDamage(float bulletDmg)
    {
        bulletDamage = bulletDmg;
    }


    void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);

        ObjectStatsManager colStats = col.gameObject.GetComponent<ObjectStatsManager>();
        if(colStats != null)
        {
            colStats.TakeDamage(bulletDamage);
        }
    }
}
