using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableItself : MonoBehaviour
{
    public bool disableParent = false;
    // Start is called before the first frame update
    public void DisableSelf()
    {
        if(disableParent){
            transform.parent.gameObject.SetActive(false);
        }else{
            gameObject.SetActive(false);
        }
    }
}
