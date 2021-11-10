using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrow : MonoBehaviour
{
    [SerializeField] float bulletDamage = 20f;
    [SerializeField] float bulletLifetime = 3f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, bulletLifetime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer != 9)
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
