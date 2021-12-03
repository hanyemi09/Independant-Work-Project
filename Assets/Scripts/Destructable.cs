using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Destructable : MonoBehaviour
{

    PhotonView photonView;
    [SerializeField] float chanceToSpawn = 50f;
    [SerializeField] Powerups[] powerup = new Powerups[4];
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void OnTriggerEnter(Collider col)
    {
        Weapon weap = col.gameObject.GetComponent<Weapon>();
        if(weap != null)
        {
            GetComponent<PhotonView>().RPC("SpawnPowerup", RpcTarget.AllBuffered);
            GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
        }
        Projectile proj = col.gameObject.GetComponent<Projectile>();
        if(proj != null)
        {
            GetComponent<PhotonView>().RPC("SpawnPowerup", RpcTarget.AllBuffered);
            GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);

        }
    }

    [PunRPC]
    void DestroyObject()
    {
        if (this.photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
    
    [PunRPC]
    void SpawnPowerup()
    {
        Debug.Log("Spawning powerup! ");
        int random = Random.Range(0, 100);
        Debug.Log(random);
        if (random <= chanceToSpawn)
        {
            Debug.Log("Spawned Powerup!");
            PhotonNetwork.Instantiate(powerup[Random.Range(0, 3)].name, gameObject.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Spawning powerup failed!");
        }
    }
}
