using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStatsManager : MonoBehaviour
{
    // sfx
    // vfx
    // death anim

    float objectHealth = 100f;
    bool isInvincible = false;

    void Update()
    {
        if (objectHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        if(!isInvincible)
        {
            objectHealth -= damage;
        }
    }


}
