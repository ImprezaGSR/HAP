using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRender2 : MonoBehaviour
{
    [SerializeField]
    [Range(1, 50)]
    private int width = 10;

    [SerializeField]
    [Range(1, 50)]
    private int height = 10;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        var maze = MazeGenerator.Generate(width, height);
        Draw(maze);
    }

    private void Draw(WallState[,] maze)
    {

        var floor = Instantiate(floorPrefab, transform);
        floor.localScale = new Vector3(width, 1, height);
        floor.position = floor.position + new Vector3(0,-size/2,-height/2 - 0.5f);

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                var cell = maze[i, j];
                var position = transform.position + new Vector3(-width / 2 + i, 0, -height + j + 0.5f);

                if (cell.HasFlag(WallState.UP))
                {
                    if(j == height -1 && i == (width/2)){

                    }else{
                        var topWall = Instantiate(wallPrefab, transform) as Transform;
                        topWall.position = position + new Vector3(0, 0, size / 2);
                        topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                    }
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (i == width - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(+size / 2, 0, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if (j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        // var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        // bottomWall.position = position + new Vector3(0, 0, -size / 2);
                        // bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);

                        if(i == (width/2)){
                            // var centralRoom = Instantiate(centralRoomPrehab, transform);
                            // centralRoom.localScale = new Vector3(size, size, size);
                            // centralRoom.position = position + new Vector3(0,0,-size*5);
                            // FindObjectOfType<PlayerMovement>().transform.position = centralRoom.Find("StartPos").position;
                        }else{
                            var bottomWall = Instantiate(wallPrefab,transform) as Transform;
                            bottomWall.position = position + new Vector3(0,0,-size/2);
                            bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);
                        }
                    }
                }
            }

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
