using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    private bool visited;
    private int risk;
    private int exits;

    public bool Visited() { return visited; }
    public void Visit() { visited = true; }

    public enum Type
    {
        START, BOSS, ITEM, LOCK, NORMAL, COLOUR
    };

    public Type type;

    public bool isActive()
    {
        return exits != 0;
    }

    public int getRisk()
    {
        return risk;
    }

    public void setRisk(int value)
    {
        risk = value;
    }

    // Frustratingly I have to store the position of a room
    private int x;
    private int y;

    public Room(int x, int y)
    {
        visited = false;
        risk = 0;
        exits = 0;
        type = Type.NORMAL;
        this.x = x;
        this.y = y;
    }

    public int getX() { return x; }
    public int getY() { return y; }

    public int ExitCount()
    {
        int tempval = exits;
        int output = 0;
        while(tempval>0)
        {
            if (tempval % 2 == 1)
                output += 1;
            tempval >>= 1;
        }
        return output;
    }

    public int GetExits()
    {
        return exits;
    }

    public void SetExits(int exits)
    {
        this.exits = exits;
    }

    public bool HasExit(Maze.Dir exit)
    {
        return (exits & (int)exit)>=1;
    }

    public void AddExit(Maze.Dir exit)
    {
        exits |= (int)exit;
    }

    public void RemoveExit(Maze.Dir exit)
    {
        exits &= ~(int)exit;
    }
}
