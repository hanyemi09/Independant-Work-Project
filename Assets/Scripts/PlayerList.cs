using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerList : MonoBehaviour
{

    List<GameObject> playerList = new List<GameObject>();

    [PunRPC]
    public List<GameObject> GetList()
    {
        return playerList;
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
    }
}
