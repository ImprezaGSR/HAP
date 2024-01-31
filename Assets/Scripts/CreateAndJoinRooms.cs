using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField joinInput;
    public int randomNum = 00000;
    public static string roomID;
    public GameObject roomAddressScene;
    public TextMeshProUGUI roomIDText;
    // Start is called before the first frame update
    public void CreateRoom(){
        randomNum = Random.Range(0, 99999);
        roomID = randomNum.ToString("00000");
        PhotonNetwork.CreateRoom(roomID);
        // roomIDText.text = roomID;
        // roomAddressScene.SetActive(true);
    }

    public void JoinRoom(){
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom(){
        PhotonNetwork.LoadLevel("SelectGame");
    }
}
