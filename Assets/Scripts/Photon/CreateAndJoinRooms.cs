using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("yeap");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("yeap");

    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }
}
