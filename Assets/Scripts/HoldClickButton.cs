using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private float pointerDownTimer;
    public float requiredHoldTime;
    public UnityEvent onPressedDown;
    public UnityEvent onPressedUp;
    

    [SerializeField]
    private Image fillImage;
    public void OnPointerDown(PointerEventData eventData){
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData){
        Reset();
        if(onPressedUp != null){
            onPressedUp.Invoke();
        }
    }

    void Update(){
        if(pointerDown){
            pointerDownTimer += Time.deltaTime;
            if(pointerDownTimer > requiredHoldTime){
                if(onPressedDown != null){
                    onPressedDown.Invoke();
                }
                Reset();
            }
            fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        }
    }

    private void Reset(){
        pointerDown = false;
        pointerDownTimer = 0;
        fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
    }

    
}
