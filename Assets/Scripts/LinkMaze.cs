using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * First pass at a wall splitting algorithm
 */
public class LinkMaze : MonoBehaviour
{
    // Links a maze to a startpoint on the map.
    int[,] maze;
    Dictionary<int, Action<int, int>> tiles;
    public CreateTiles ct;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        maze = new int[,]{
            {1, 1, 1, 1 },
            {1, 0, 0, 1 },
            {1,1,1,1 }
        };

        tiles = new Dictionary<int, Action<int, int>>();
        tiles.Add(0, ct.Empty);
        tiles.Add(1, ct.Fill);
    }

    private void GenerateMaze()
    {
        // Create a maze to store in the maze array
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            Vector3Int tileStart = ct.GetTilePos(player.position);
            maze = MakeMaze.RecursiveDivision(20, 20, 5, 5);
            Link(tileStart.x, tileStart.y);
        }
    }

    public void Link(int startX, int startY)
    {
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = rMax; i >= 0; i--)
        {
            for(int j = 0; j<=cMax; j++)
            {
                tiles[maze[i, j]](j+startX, i+startY);
            }
        }
    }
}
