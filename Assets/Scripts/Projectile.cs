using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float bulletDamage = 20f;
    public float bulletLifetime = 3f;
    public float bulletSpeed = 20f;
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
