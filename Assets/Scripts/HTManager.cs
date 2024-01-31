using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HTManager : MonoBehaviour
{
    public DialogueManager dManager;
    public bool isLux = true;
    public int CameraLevel = 0;
    public float levelUpZoom = 5f;
    public int SpeedLevel = 0;
    public float levelUpSpeed = 0.5f;
    public int TimeLevel = 0;
    public float levelUpTime = 15f;
    public int maxLevel = 5;
    public float timeMax = 30;
    private float timeCurrent;

    public SmoothCameraFollow cameraFollow;
    private PlayerMovement playerMovement;

    public GameObject UILux;
    public GameObject UITenebris;
    private GameObject playerUI;

    void Start(){
        UILux.SetActive(false);
        // UITenebris.SetActive(false);
        Debug.Log("Room Address: "+CreateAndJoinRooms.roomID);
        
        playerMovement = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
        if(isLux){
            playerUI = UILux;
        }else{
            playerUI = UITenebris;
        }
    }

    public void ActivateUpgradeUI(){
        playerUI.SetActive(true);
        playerUI.transform.Find("Upgrade").gameObject.SetActive(true);
    }

    public void DeactivateUpgradeUI(){
        playerUI.transform.Find("Dialogue").gameObject.SetActive(false);
        playerUI.transform.Find("Upgrade").gameObject.SetActive(false);
        playerUI.SetActive(false);
    }

    public void ActivateDialogue(string sentence){
        playerUI.SetActive(true);
        playerUI.transform.Find("Dialogue").gameObject.SetActive(true);
        dManager.SetSentence(playerUI.transform.Find("Dialogue").Find("Text").GetComponent<TextMeshProUGUI>(), sentence);
    }

    public void Upgrade(int index){
        if(index == 0){
            if(CameraLevel < maxLevel){
                CameraLevel += 1;
                playerUI.transform.Find("Upgrade").GetChild(index).Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Lvl."+(CameraLevel+1);
                cameraFollow.ZoomOut(levelUpZoom, -levelUpZoom/2);
                if(CameraLevel >= maxLevel){
                    playerUI.transform.Find("Upgrade").GetChild(index).Find("UpgradeButton").gameObject.SetActive(false);
                }
            }
        }
        else if(index == 1){
            if(SpeedLevel < maxLevel){
                SpeedLevel += 1;
                playerUI.transform.Find("Upgrade").GetChild(index).Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Lvl."+(SpeedLevel+1);
                playerMovement.speed += levelUpSpeed;
                if(SpeedLevel >= maxLevel){
                    playerUI.transform.Find("Upgrade").GetChild(index).Find("UpgradeButton").gameObject.SetActive(false);
                }
            }
        }
        else if(index == 2){
            if(TimeLevel < maxLevel){
                TimeLevel += 1;
                playerUI.transform.Find("Upgrade").GetChild(index).Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Lvl."+(TimeLevel+1);
                timeMax += levelUpTime;
                if(TimeLevel >= maxLevel){
                    playerUI.transform.Find("Upgrade").GetChild(index).Find("UpgradeButton").gameObject.SetActive(false);
                }
            }
        }
        
    }
}
