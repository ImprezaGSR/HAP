using Unity.Netcode; 
using UnityEngine; 
public class ModeButtons : MonoBehaviour{ 
        void OnGUI() 
        { 
           GUILayout.BeginArea(new Rect(30, 30, 300, 300)); 
           if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer) 
            { 
                if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost(); 
                if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer(); 
                if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient(); 
            } 
            GUILayout.EndArea(); 
        } 
}
