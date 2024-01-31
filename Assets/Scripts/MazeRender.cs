using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRender : MonoBehaviour
{
    [SerializeField]
    [Range(1,50)]
    private int width = 10;
    [SerializeField]
    [Range(1,50)]
    private int height = 10;
    [SerializeField]
    private float size = 1f;
    [SerializeField]
    private Transform wallPrehab = null;
    [SerializeField]
    private Transform endWallPrehab = null;
    [SerializeField]
    private Transform floorPrehab = null;
    // [SerializeField]
    // private Transform spawnerPrehab = null;
    private int spawnX;
    private int spawnY;
    // public List<Transform> Chests;

    // [SerializeField]
    // private Transform keyPrehab = null;
    // [Range(1,5)]
    // public int keyNum;
    // private bool keySpawned = false; 
    // [SerializeField]
    // private Transform playerPrehab = null;
    // [SerializeField]
    // private Transform exitPrehab = null;
    // public bool playerSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        var maze = MazeGenerator.Generate(width,height);
        Draw(maze);
    }
    private void Draw(WallState[,]maze){
        var floor = Instantiate(floorPrehab, transform);
        floor.localScale = new Vector3((width) * size,1,(height) * size);
        floor.position = floor.position + new Vector3(0,-size/2,0);
        for(int i=0; i<width; ++i){
            for(int j=0; j<width; ++j){
                var cell = maze[i,j];
                // var spawner = Instantiate(spawnerPrehab, transform) as Transform;
                // spawner.localScale = new Vector3(spawner.localScale.x * size, spawner.localScale.y * size, spawner.localScale.z * size);
                var position = transform.position + new Vector3((-width/2 + i) * size, 0, (-height/2 + j) * size);
                // spawner.position = position;
                if(cell.HasFlag(WallState.UP)){
                    if(j == height - 1){
                        var topWall = Instantiate(endWallPrehab,transform) as Transform;
                        topWall.position = position + new Vector3(0,0,size/2);
                        topWall.localScale = new Vector3(size, topWall.localScale.y * size, topWall.localScale.z * size);
                    }else{
                        var topWall = Instantiate(wallPrehab,transform) as Transform;
                        topWall.position = position + new Vector3(0,0,size/2);
                        topWall.localScale = new Vector3(size, topWall.localScale.y * size, topWall.localScale.z * size);
                    }
                }
                if(cell.HasFlag(WallState.LEFT)){
                    if(i == 0){
                        var leftWall = Instantiate(endWallPrehab, transform) as Transform;
                        leftWall.position = position + new Vector3(-size/2,0,0);
                        leftWall.localScale = new Vector3(size, leftWall.localScale.y * size, leftWall.localScale.z * size);
                        leftWall.eulerAngles = new Vector3(0,90,0);
                    }else{
                        var leftWall = Instantiate(wallPrehab, transform) as Transform;
                        leftWall.position = position + new Vector3(-size/2,0,0);
                        leftWall.localScale = new Vector3(size, leftWall.localScale.y * size, leftWall.localScale.z * size);
                        leftWall.eulerAngles = new Vector3(0,90,0);
                    }
                }
                if(i == width -1){
                    if(cell.HasFlag(WallState.RIGHT)){
                        var rightWall = Instantiate(wallPrehab, transform) as Transform;
                        rightWall.position = position + new Vector3(size/2,0,0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z * size);
                        rightWall.eulerAngles = new Vector3(0,90,0);
                    }
                }
                if(j == 0){
                    if(cell.HasFlag(WallState.DOWN)){
                        if(i == (width/2)){
                            // var centralRoom = Instantiate(centralRoomPrehab, transform);
                            // centralRoom.localScale = new Vector3(size, size, size);
                            // centralRoom.position = position + new Vector3(0,0,-size*5);
                            // FindObjectOfType<PlayerMovement>().transform.position = centralRoom.Find("StartPos").position;
                        }else{
                            var bottomWall = Instantiate(endWallPrehab,transform) as Transform;
                            bottomWall.position = position + new Vector3(0,0,-size/2);
                            bottomWall.localScale = new Vector3(size, bottomWall.localScale.y * size, bottomWall.localScale.z * size);
                        }
                    }
                }
            }
        }
    }

    // private void SpawnKey(){
    //     keySpawned = true;
    //     // if (keyNum > Chests.Count){
    //     //     keyNum = Chests.Count;
    //     // }
    //     int chestRand = Random.Range(0,Chests.Count-1);
    //     var player = Instantiate(playerPrehab, Chests[chestRand].position,Chests[chestRand].rotation);
    //     var exit = Instantiate(exitPrehab, Chests[chestRand].position,Chests[chestRand].rotation);

    //     while(keyNum >= 0){
    //         chestRand = Random.Range(0,Chests.Count-1);
    //         Debug.Log(chestRand);
    //         var key = Instantiate(keyPrehab, Chests[chestRand].position,Chests[chestRand].rotation);
    //         keyNum--;
    //     }
    // }

    // public void AddChest(Vector3 chestTransform){
    //     Chests.Add(chestTransform);
    // }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Chests.Count);
        // if(Chests.Count > 0 && !keySpawned){
        //     SpawnKey();
        // }
    }
}
