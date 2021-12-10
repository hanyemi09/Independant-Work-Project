using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class RespawnPlayer : MonoBehaviour
{

    [SerializeField] float respawnTimer = 2f;
    [SerializeField] float respawnTimerUI;
    [SerializeField] TextMeshProUGUI tmp;
    bool isRespawning = false;
    // Start is called before the first frame update
    void Start()
    {
        respawnTimerUI = respawnTimer;
        //tmp = GameObject.Find("/Canvas/UI/RespawnTimer").GetComponent<TextMeshProUGUI>();
        //tmp.text = respawnTimerUI.ToString();
        //tmp.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {


        //respawnTimerUI = Mathf.Round(respawnTimerUI * 100f) / 100f;
        

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf == false)
            {
                if(!isRespawning)
                {
                    CallRespawnPlayer(gameObject.transform.GetChild(i).GetComponent<PhotonView>().ViewID);
                    isRespawning = true;
                }
                else
                {
                    Debug.Log("TEXTTEXTTEXT");
                    respawnTimerUI -= Time.deltaTime;
                    //respawnTimerUI = Mathf.Round(respawnTimerUI);
                    tmp.text = respawnTimerUI.ToString("f1");
                }
            }
        }
    }

    void RespawnTimerUI()
    {
        respawnTimerUI = respawnTimer;
        Debug.Log("TMP enabled ");
        tmp.enabled = true;
    }

    void CallRespawnPlayer(int ID)
    {
        StartCoroutine(RespawnDaPlayer(respawnTimer, ID));
    }


    IEnumerator RespawnDaPlayer(float respawnTimer, int ID)
    {
        RespawnTimerUI();
        Debug.Log("Entering");
        yield return new WaitForSeconds(respawnTimer);
        Debug.Log("Exiting");
        tmp.enabled = false;
        RespawnDemPlayer(ID);
    }

    void RespawnDemPlayer(int ID)
    {
        ObjectStatsManager osm = gameObject.transform.GetChild(0).gameObject.GetComponent<ObjectStatsManager>();
        osm.RespawnDaPlayer();
        isRespawning = false;
    }
}
