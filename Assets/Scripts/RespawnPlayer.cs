using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RespawnPlayer : MonoBehaviour
{

    [SerializeField] float respawnTimer = 2f;
    bool isRespawning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.active == false)
            {
                if(!isRespawning)
                {
                    CallRespawnPlayer(gameObject.transform.GetChild(i).GetComponent<PhotonView>().ViewID);
                    isRespawning = true;
                }
            }
        }

    }
    
    void CallRespawnPlayer(int ID)
    {
        StartCoroutine(RespawnDaPlayer(respawnTimer, ID));
    }


    IEnumerator RespawnDaPlayer(float respawnTimer, int ID)
    {
        Debug.Log("Entering");
        yield return new WaitForSeconds(respawnTimer);
        Debug.Log("Exiting");
        RespawnDemPlayer(ID);
    }

    void RespawnDemPlayer(int ID)
    {
        ObjectStatsManager osm = gameObject.transform.GetChild(0).gameObject.GetComponent<ObjectStatsManager>();
        osm.RespawnDaPlayer();
        isRespawning = false;
    }
}
