using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate : MonoBehaviour
{
    [SerializeField] Rigidbody bulletPrefab;
    Transform shootPoint;

    float distanceMultiplier = 10f;
    float time = 10f;

    void Start()
    {
        shootPoint = GameObject.Find("ShootPoint").transform;
    }

    void TryThrowGrenade(Vector3 shootDir)
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

        Rigidbody rb = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        rb.velocity = result;

    }
}
