using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PowerupsSpawn : MonoBehaviour
{
    [SerializeField] float chanceToSpawn = 50f;
    [SerializeField] Powerups[] powerup = new Powerups[4];

    [PunRPC]
    void SpawnPowerup(Transform transform)
    {
        Debug.Log("Spawning powerup! ");
        int random = Random.Range(0, 100);
        Debug.Log(random);
        if (random <= chanceToSpawn)
        {
            Debug.Log("Spawned Powerup!");
            PhotonNetwork.Instantiate(powerup[Random.Range(0, 3)].name, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Spawning powerup failed!");
        }
    }
}
