using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    [SerializeField] SpawnPoint[] spawnPoints = new SpawnPoint[4];
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public PlayerList pl;

    void Awake()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[i].transform.position, Quaternion.identity);
        }
        //Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minZ, maxZ));
        //PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }
}
