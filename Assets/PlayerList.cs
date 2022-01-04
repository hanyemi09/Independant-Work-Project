using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour
{

    GameObject[] currentPlayers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayer(GameObject go)
    {
        if(currentPlayers.Length == 0)
        {
            currentPlayers[0] = go;
        }
        else
        {
            for (int i = 0; i < currentPlayers.Length; i++)
            {
                if (currentPlayers[i] != null)
                {
                    currentPlayers[i] = go;
                }
            }
        }


    }

    public GameObject[] GetPlayers()
    {
        return currentPlayers;
    }
}
