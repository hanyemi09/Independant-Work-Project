using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createRoomText;
    public InputField joinRoomText;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomText.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomText.text);

    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }
}
