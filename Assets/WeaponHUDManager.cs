using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHUDManager : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerWeaponsManager m_PlayerWeaponsManager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(GameObject player)
    {
        m_PlayerWeaponsManager = player.GetComponent<PlayerWeaponsManager>();
    }
}
