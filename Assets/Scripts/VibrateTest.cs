using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateTest : MonoBehaviour
{
    public void Touch(){
        HapticManager.Vibrate();
    }
}
