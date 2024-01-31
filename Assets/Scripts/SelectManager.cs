using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SelectManager : MonoBehaviour
{
    public GameObject roomAddress;
    public GameObject buttonRoom;
    public TextMeshProUGUI connectedText;
    public TextMeshProUGUI roomIDText;
    public HoldClickButton button;
    public bool buttonPressed = false;
    public bool otherButtonPressed = false;
    public Animator buttonAnimator;
    private bool isJoined;

    private bool isDisconnecting = false;

    PhotonView view;

    void Start(){
        view = GetComponent<PhotonView>();
        
        roomIDText.text = CreateAndJoinRooms.roomID;
    }

    void Update(){
        if(PhotonNetwork.PlayerList.Length == 2){
            connectedText.text = "Connected with Other Player!";
            roomAddress.SetActive(false);
        }

        if(isJoined && PhotonNetwork.PlayerList.Length < 2){
            StartCoroutine(DisconnectAndLoad());
        }

        // if(button.){
        //     // if(Input.GetMouseButtonUp(0)){
        //     //     buttonPressed = false;
        //     // }
        // }

        buttonAnimator.SetBool("Pressed", buttonPressed);

        if(buttonPressed && otherButtonPressed){
            buttonRoom.SetActive(false);
            HapticManager.Vibrate();
            buttonPressed = false;
            otherButtonPressed = false;
        }
        
    }

    public void EndHosting(){
        if(PhotonNetwork.PlayerList.Length < 2){
            buttonRoom.SetActive(false);
            roomAddress.SetActive(false);
        }else{
            view.RPC(nameof(ToButtonRoom), RpcTarget.All);
            isJoined = true;
        }
    }

    public void RPCToGameScene(int index){
        if(index == 0){
            view.RPC(nameof(ToHideAndSeek), RpcTarget.All);
        }
        if(index == 1){
            view.RPC(nameof(ToHoldTold), RpcTarget.All);
        }
    }

    [PunRPC]
    public void ToButtonRoom(){
        roomAddress.SetActive(false);
    }
    
    [PunRPC]
    public void ToHideAndSeek(){
        SceneManager.LoadScene("HideAndSeek");
    }

    [PunRPC]
    public void ToHoldTold(){
        SceneManager.LoadScene("HoldTold");
    }

    public void SetButtonPressed(){
        buttonPressed = true;
        view.RPC(nameof(SetOtherButtonPressed), RpcTarget.Others, buttonPressed);
    }

    public void SetButtonUnPressed(){
        buttonPressed = false;
        view.RPC(nameof(SetOtherButtonPressed), RpcTarget.Others, buttonPressed);
    }

    [PunRPC]
    public void SetOtherButtonPressed(bool pressed){
        otherButtonPressed = pressed;
    }

    IEnumerator DisconnectAndLoad(){
        if(!isDisconnecting){
            PhotonNetwork.LeaveRoom();
            while(PhotonNetwork.InRoom){
                yield return null;
            }
            SceneManager.LoadScene("MainMenu");
            isDisconnecting = true;
        }
    }
}
