using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour
{
    Rigidbody rb;
    Projectile proj;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        proj = GetComponent<Projectile>();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Block"))
        {
            Vector3 wallNormal = other.contacts[0].normal;
            Vector3 dir = Vector3.Reflect(rb.velocity, wallNormal).normalized;
            rb.velocity = dir * 2;
        }
    }
}
