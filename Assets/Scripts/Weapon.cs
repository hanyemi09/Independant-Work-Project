using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour 
{
    private enum WEAPONTYPE {
        MELEE, RANGED
    };

    public GameObject bulletPrefab;
    public float weaponFireRate = 0.5f;

    Transform shootPoint;
    [SerializeField]WEAPONTYPE weaponType;
    float timeToNextAttack = 0f;

    WEAPONTYPE GetWeaponType()    
    {
        return weaponType;
    }

    void Start()
    {
        shootPoint = GameObject.Find("ShootPoint").GetComponent<Transform>();
    }
    
    void Update()
    {
        timeToNextAttack += Time.deltaTime;
    }

    public void WeaponAttack()
    {
        if(weaponType == WEAPONTYPE.MELEE)
        {

        }
        else if(weaponType == WEAPONTYPE.RANGED)
        {
            WeaponShoot();
        }
    }

    void WeaponShoot()
    {
        if(timeToNextAttack > weaponFireRate)
        {
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            timeToNextAttack = 0;
        }
    }
}
