using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MakeMaze
{
    private static int minWidth;
    private static int minHeight;
    public static void test()
    {
        
    }

    public static int[,] RecursiveDivision(int width, int height, int minWidth, int minHeight)
    {
        int[,] maze = new int[height, width];

        //Actually generate the maze
        MakeMaze.minHeight = minHeight;
        MakeMaze.minWidth = minWidth;

        divide(ref maze, 0, 0, width, height, chooseOrient(width, height));

        return maze;
    }

    private enum Orient
    {
        HORI, VERT
    };

    private enum Dir
    {
        N, E, S, W
    };

    private static Orient chooseOrient(int width, int height)
    {
        if(width< height)
        {
            return Orient.HORI;
        }
        else if(width > height)
        {
            return Orient.VERT;
        }
        else
        {
            return getRandom();
        }
    }

    private static Orient getRandom()
    {
        return Random.Range(0, 1) == 0 ? Orient.HORI : Orient.VERT;
    }

    private static void divide(ref int[,] maze, int x, int y, int width, int height, Orient o)
    {
        if (width < minWidth || height < minHeight)
            return;

        bool hori = (o == Orient.HORI);

        // Find wall drawing position
        int wx = x + (hori ? 0 : Random.Range(0, width - 2));
        int wy = y + (hori ? Random.Range(0, height - 2) : 0);

        // Find passage location
        int px = wx + (hori ? Random.Range(0, width) : 0);
        int py = wy + (hori ? 0 : Random.Range(0, height));

        // Direction of wall?
        int dx = (hori ? 1 : 0);
        int dy = (hori ? 0 : 1);

        // Length of wall
        int length = hori ? width : height;

        // Direction opposite to wall
        Dir d = hori ? Dir.S: Dir.E;

        for(int i=0; i<length; i++)
        {
            if (wx != px || wy != py)
            {
                maze[wy, wx] = 1;
            }
            wx += dx;
            wy += dy;
        }

        int nx = x;
        int ny = y;

        int w = hori ? width : wx - x + 1;
        int h = hori ? wy - y + 1 : height;

        // Divide first half
        divide(ref maze, nx, ny, w, h, chooseOrient(w, h));

        nx = hori ? x : wx + 1;
        ny = hori ? wy + 1 : y;

        w = hori ? width : x + width - wx - 1;
        h = hori ? y + height - wy - 1 : height;
        divide(ref maze, nx, ny, w, h, chooseOrient(w, h));



        // Issue, need to adjust code for drawing walls rather than passages
    }
}
