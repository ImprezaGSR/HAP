using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RippleSpawner : MonoBehaviour
{
    Touch touch;
    Vector3 touchPosition;
    public GameObject ripplePrehab;
    public GameObject hidePrehab;
    public Camera mainCam;
    GameManager gameManager;
    public bool canSpawnRipple = false;
    private bool isHiding = false;

    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawnRipple){
            if(Input.touchCount > 0){
                touchPosition = mainCam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, mainCam.nearClipPlane+9));
                view.RPC(nameof(SpawnRippleTouch), RpcTarget.All, touchPosition);
            }
            if(Input.GetMouseButtonDown(0)){
                touchPosition = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.nearClipPlane+9));
                view.RPC(nameof(SpawnRippleClick), RpcTarget.All, touchPosition);
            }
        }

    }

    [PunRPC]
    public void SpawnRippleTouch(Vector3 pos){
        // touch = Input.GetTouch(0);
        // if(touch.phase == TouchPhase.Began){
        //     pos.z = 0;
        //     if(isHiding){
        //         Instantiate(ripplePrehab, pos, Quaternion.identity);
        //     }else{
        //         Instantiate(hidePrehab, pos, Quaternion.identity);
        //         gameManager.StartHunt();
        //         isHiding = true;
        //     }
        // }
        // canSpawnRipple = false;
    }

    [PunRPC]
    public void SpawnRippleClick(Vector3 pos){
        // touchPosition = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.nearClipPlane+9));
        pos.z = 0;
        Debug.Log(pos);
        if(isHiding){
            Instantiate(ripplePrehab, pos, Quaternion.identity);
        }else{
            Instantiate(hidePrehab, pos, Quaternion.identity);
            gameManager.StartHunt();
            isHiding = true;
        }
        canSpawnRipple = false;
    }
}
