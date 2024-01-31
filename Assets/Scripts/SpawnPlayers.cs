using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrehab;

    public float minX;
    public float macX;
    public float minY;
    public float maxY;

    private void Start(){
        PhotonNetwork.Instantiate(playerPrehab.name, Vector2.zero, Quaternion.identity);
    }
}
