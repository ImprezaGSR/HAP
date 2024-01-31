using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleRing : MonoBehaviour
{
    public float growRate = 1f;

    public bool noVibrate = false;

    float currentTime = 0f;

    private GameManager gameManager;
    
    void Awake()
    {
        transform.localScale = Vector3.zero;
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime * growRate;
        float currentScale = Mathf.Lerp(0,1,currentTime);
        transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        if(currentTime >= 1){
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("RippleC")){
            if(!noVibrate){
                Debug.Log("Found Presence of Hider");
                HapticManager.Cancel();
                HapticManager.Vibrate();
            }
        }
    }
}
