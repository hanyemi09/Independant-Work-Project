using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class PlayerList : MonoBehaviour
{

    List<GameObject> playerList = new List<GameObject>();
    [SerializeField] GameObject m_GameEnd;
    Canvas m_Canvas;

    void Start()
    {
        m_Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
    void Update()
    {

    }

    public void GameEnd()
    {
        Instantiate(m_GameEnd, m_Canvas.transform);
    }

    [PunRPC]
    public List<GameObject> GetList()
    {
        return playerList;
    }

    public int GetPlayerIndexByViewID(int ViewID)
    {
        for(int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].gameObject.GetComponent<PhotonView>().ViewID == ViewID)
            {
                return i;
            }
        }

        return -1;
    }
    public GameObject GetPlayerByViewID(int ViewID)
    {
       GameObject go = PhotonView.Find(ViewID).gameObject;
       return go;
    }
    public GameObject GetPlayerByIndex(int index)
    {
        return playerList[index];
    }
    public int GetListLength()
    {
        return playerList.Count;
    }
    public void AddToList(GameObject Player)
    {
        playerList.Add(Player);
    }

    public void RemoveFromList(GameObject Player)
    {
        playerList.Remove(Player);
        if(playerList.Count <= 1)
        {
            GameEnd();
        }
    }
}
