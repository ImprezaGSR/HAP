using UnityEngine;

public static class HapticManager
{
#if UNITY_ANDROID && !UNITY_EDITOR 
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject hapticManager = currentActivity.Call<AndroidJavaObject>("getSystemService","hapticManager");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject hapticManager;
#endif

    public static void Vibrate(long milliseconds = 250){
        if(IsAndroid()){
            hapticManager.Call("vibrate", milliseconds);
        }else{
            Handheld.Vibrate();
        }
    }

    public static void Cancel(){
        if(IsAndroid()){
            hapticManager.Call("cancel");
        }
    }

    public static bool IsAndroid(){
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }
}
