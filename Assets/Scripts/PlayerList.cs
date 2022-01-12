using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerList : MonoBehaviour
{

    List<GameObject> playerList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public List<GameObject> GetList()
    {
        return playerList;
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
