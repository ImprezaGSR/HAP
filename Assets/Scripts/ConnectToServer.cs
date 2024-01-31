using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    private bool isConnecting = false;
    public GameObject disclaimer;
    // Start is called before the first frame update
    void Start()
    {
        // PhotonNetwork.ConnectUsingSettings();
        Invoke("StartConnecting", 8.0f);
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            disclaimer.SetActive(false);
            StartConnecting();
        }
    }

    public void StartConnecting()
    {
        if(!isConnecting){
            PhotonNetwork.ConnectUsingSettings();
            isConnecting = true;
        }
    }

    public override void OnConnectedToMaster(){
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby(){
        SceneManager.LoadScene("MainMenu");
    }
}
