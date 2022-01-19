using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    [SerializeField] SpawnPoint[] spawnPoints = new SpawnPoint[4];
    PhotonView m_PhotonView;
    PlayerList m_PlayerList;
    
    int m_Index = 0;
    void Awake()
    {
        m_PlayerList = GetComponent<PlayerList>();
        m_PhotonView = GetComponent<PhotonView>();   
    }

    void Start()
    {
        PlayerWeaponsController player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[m_Index].transform.position, Quaternion.identity).GetComponent<PlayerWeaponsController>();
        int i = player.gameObject.GetComponent<PhotonView>().ViewID;
        m_PhotonView.RPC("SetPlayersStartingPosition", RpcTarget.AllBuffered, i );

    }

    [PunRPC]
    public void SetPlayersStartingPosition(int playerViewID)
    {
        
        PhotonView.Find(playerViewID).gameObject.transform.position = spawnPoints[m_Index].transform.position;
        m_Index += 1;
        if (m_Index > 3)
        {
            m_Index = 0;
        }

    }

    public PhotonView GetPhotonView()
    {
        return m_PhotonView;
    }
}
