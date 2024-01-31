using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleCenter : MonoBehaviour
{
    public float growRate = 0.4f;
    public int spawnNum = 3;
    public float spawnRate = 0.4f;
    public GameObject rippleRing; 
    private GameManager gameManager;

    float currentTime = 0f;
    float remainingNum;

    private bool hasFound = false;
    
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        remainingNum = spawnNum;
        transform.localScale = Vector3.zero;
        StartCoroutine(SpawnRing());
    }

    // Update is called once per frame
    void Update()
    {
        if(remainingNum > 0){
            currentTime += Time.deltaTime * growRate;
            
        }else{
            currentTime -= Time.deltaTime * growRate;
            if(currentTime <= 0){
                if(hasFound){
                    gameManager.HunterWon();
                    HapticManager.Cancel();
                    HapticManager.Vibrate();
                }else{
                    gameManager.ToNextAttempt();
                }
                Destroy(gameObject);
            }
        }
        float currentScale = Mathf.Lerp(0,1,currentTime);
        transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }
    IEnumerator SpawnRing(){
        for (int i = 0; i < spawnNum; i++){
            var ring = Instantiate(rippleRing, transform.position, Quaternion.identity);
            if(hasFound){
                ring.gameObject.GetComponent<RippleRing>().noVibrate = true;
            }
            remainingNum -= 1;
            Debug.Log("SpawnRing");
            yield return new WaitForSeconds(spawnRate);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("RippleC")){
            Debug.Log("Found Hider");
            hasFound = true;
        }
    }
}
