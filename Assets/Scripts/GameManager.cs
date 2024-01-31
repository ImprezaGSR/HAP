using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public bool isHunter = true;
    public RectTransform heartHolder;
    public RectTransform heartImage;
    public RectTransform checkHolder;
    public RectTransform checkImage;
    public int healthNum = 3;
    [SerializeField]
    private int healthNow;
    public float heartMargin = 100f;
    public Animator canvasAnimator;
    public TextMeshProUGUI centerText;
    public TextMeshProUGUI topText;
    public bool canSpawnRipple = true;
    private RippleSpawner rippleSpawner;
    private bool isReady = false;
    private bool isMasterReady = false;
    private bool isDisconnecting = false;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Room Address: "+CreateAndJoinRooms.roomID);
        view = GetComponent<PhotonView>();
        if(PhotonNetwork.PlayerList.Length < 2){
            topText.text = "Please wait for the other player...";
            checkHolder.gameObject.SetActive(false);
            heartHolder.gameObject.SetActive(false);
        }
        rippleSpawner = FindObjectOfType<RippleSpawner>().GetComponent<RippleSpawner>();
        Debug.Log(rippleSpawner);
        // if(isHunter){
        //     SetCenterText("You are the Hunter!");
        //     topText.text = "Please wait for the hider...";
        //     heartHolder.gameObject.SetActive(true);
        //     checkHolder.gameObject.SetActive(false);
        // }else{
        //     SetCenterText("You are the Hider!");
        //     topText.text = "Tap any area to hide!";
        //     heartHolder.gameObject.SetActive(false);
        //     checkHolder.gameObject.SetActive(true);
        //     checkHolder.GetChild(0).Find("CheckImage").gameObject.SetActive(false);
        // }
        
        // healthNow = healthNum;
        // if(healthNum > 1){
        //     for(int i = 1; i < healthNum; i++){
        //         if(isHunter){
        //             Vector3 newTrans = new Vector3(heartImage.position.x + i * heartMargin, heartImage.position.y, heartImage.position.z);
        //             var heart = Instantiate(heartImage, heartHolder);
        //             heart.position = newTrans;
        //         }else{
        //             Vector3 newTrans = new Vector3(checkImage.position.x + i * heartMargin, checkImage.position.y, checkImage.position.z);
        //             var check = Instantiate(checkImage, checkHolder);
        //             check.position = newTrans;
        //             check.transform.GetChild(0).gameObject.SetActive(false);
        //         }
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.PlayerList.Length == 2 && !isReady){
            if(PhotonNetwork.IsMasterClient){
                isHunter = (Random.value > 0.5f);
                isHunter = false;
                Debug.Log(isHunter);
                view.RPC(nameof(RPCRoles), RpcTarget.Others, isHunter);
            }else{
                if(!isMasterReady){
                    return;
                }
                Debug.Log("Your role: "+isHunter);
            }
            if(isHunter){
                SetCenterText("You are the Hunter!");
                topText.text = "Please wait for the hider...";
                heartHolder.gameObject.SetActive(true);
                checkHolder.gameObject.SetActive(false);
            }else{
                SetCenterText("You are the Hider!");
                topText.text = "Tap any area to hide!";
                heartHolder.gameObject.SetActive(false);
                checkHolder.gameObject.SetActive(true);
                checkHolder.GetChild(0).Find("CheckImage").gameObject.SetActive(false);
            }
            
            healthNow = healthNum;
            if(healthNum > 1){
                for(int i = 1; i < healthNum; i++){
                    if(isHunter){
                        Vector3 newTrans = new Vector3(heartImage.position.x + i * heartMargin, heartImage.position.y, heartImage.position.z);
                        var heart = Instantiate(heartImage, heartHolder);
                        heart.position = newTrans;
                    }else{
                        Vector3 newTrans = new Vector3(checkImage.position.x + i * heartMargin, checkImage.position.y, checkImage.position.z);
                        var check = Instantiate(checkImage, checkHolder);
                        check.position = newTrans;
                        check.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
            StartCoroutine(StartHiding());
            isReady = true;
        }

        if(isReady && PhotonNetwork.PlayerList.Length != 2){
            StartCoroutine(DisconnectAndLoad());
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            if(!isHunter){
                view.RPC(nameof(RPCStartHunt), RpcTarget.All);
            }
        }
    }
    
    public void ReduceHealth(){
        if(healthNow > 0){
            if(isHunter){
                healthNow -= 1;
                heartHolder.GetChild(healthNow).Find("HeartImage").gameObject.SetActive(false);
            }else{
                checkHolder.GetChild(healthNum - healthNow).Find("CheckImage").gameObject.SetActive(true);
                healthNow -= 1;
            }
        }
    }

    public void ToNextAttempt(){
        // view.RPC(nameof(RPCToNextAttempt), RpcTarget.All);
        ReduceHealth();
        centerText.text = "Attempt "+((healthNum - healthNow)+1);
        if(healthNow == 0){
            Debug.Log("GameOver");
            // view.RPC(nameof(RPCFinishHunt), RpcTarget.All, false);
            StartCoroutine(Finish(false));
        }else{
            canvasAnimator.SetTrigger("NextAttempt");
            if(isHunter){
                Invoke("CanRippleAgain",2f);
            }
        }
    }

    // [PunRPC]
    // public void RPCToNextAttempt(){
    //     ReduceHealth();
    //     centerText.text = "Attempt "+((healthNum - healthNow)+1);
    //     if(healthNow == 0){
    //         Debug.Log("GameOver");
    //         view.RPC(nameof(RPCFinishHunt), RpcTarget.All, false);
    //     }else{
    //         canvasAnimator.SetTrigger("NextAttempt");
    //         if(isHunter){
    //             Invoke("CanRippleAgain",2f);
    //         }
    //     }
    // }

    public void HunterWon(){
        // view.RPC(nameof(RPCFinishHunt), RpcTarget.All, true);
        StartCoroutine(Finish(true));
    }

    private void SetCenterText(string text){
        centerText.text = text;
        canvasAnimator.SetTrigger("CenterText");
    }

    private void CanRippleAgain(){
        rippleSpawner.canSpawnRipple = true;
        Debug.Log("CanRippleAgain");
    }

    [PunRPC]
    public void RPCRoles(bool isMasterHunter){
        Debug.Log("Master's role: "+isMasterHunter);
        if(isMasterHunter){
            isHunter = false;
        }else{
            isHunter = true;
        }
        isMasterReady = true;
        // Debug.Log("Your role: "+isHunter);
    }

    [PunRPC]
    public void RPCStartHunt(){
        StartCoroutine(_StartHunt());
    }

    public void StartHunt(){
        StartCoroutine(_StartHunt());
    }

    [PunRPC]
    public void RPCFinishHunt(bool hunter){
        StartCoroutine(Finish(hunter));
    }

    IEnumerator StartHiding(){
        yield return new WaitForSeconds(2f);
        if(!isHunter){
            CanRippleAgain();
        }
    }

    IEnumerator _StartHunt(){
        yield return new WaitForSeconds(1f);
        if(isHunter){
            SetCenterText("Find Hider!");
            topText.text = "Find Hider!";
        }else{
            SetCenterText("Hider's turn!");
            topText.text = "You're Hiding";
            // canSpawnRipple = false;
        }
        
        yield return new WaitForSeconds(2f);
        centerText.text = "Attempt 1";
        canvasAnimator.SetTrigger("NextAttempt");
        yield return new WaitForSeconds(3.5f);
        if(isHunter){
            CanRippleAgain();
        }
    }

    IEnumerator Finish(bool hunter){
        SetCenterText("Finish!");
        yield return new WaitForSeconds(2f);
        if(hunter){
            SetCenterText("Hunter won!");
        }else{
            SetCenterText("Hider won!");
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(DisconnectAndLoad());
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
