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

    public void WeaponShoot()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }

    public float GetWeaponFireRate()
    {
        return weaponFireRate;
    }

}
