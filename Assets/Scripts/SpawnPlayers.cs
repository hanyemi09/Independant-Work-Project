using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    [SerializeField] SpawnPoint[] spawnPoints = new SpawnPoint[4];
    PhotonView m_PhotonView;

    int i = 0;
    void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();   
        PlayerWeaponsController player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[i].transform.position, Quaternion.identity).GetComponent<PlayerWeaponsController>();
        //m_PhotonView.RPC("initializeid", RpcTarget.AllBuffered, player.GetComponent<PhotonView>().ViewID);
        i += 1;
        //Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minZ, maxZ));
        //PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }

    [PunRPC]
    void initializeid(int id)
    {
        PhotonView.Find(id).GetComponent<PlayerWeaponsController>().Initialize();

    }
}
