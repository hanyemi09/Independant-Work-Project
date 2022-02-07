using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PowerupsSpawn : MonoBehaviour
{
    [SerializeField] float chanceToSpawn = 50f;
    [SerializeField] Pickup[] powerup = new Pickup[2];

    [PunRPC]
    public void SpawnPowerup()
    {
        Debug.Log("Spawning powerup! ");
        int random = Random.Range(0, 100);
        Debug.Log(random);
        if (random <= chanceToSpawn)
        {
            Debug.Log("Spawned Powerup!");
            PhotonNetwork.Instantiate(powerup[Random.Range(0, powerup.Length)].name, this.gameObject.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Spawning powerup failed!");
        }
    }
}
