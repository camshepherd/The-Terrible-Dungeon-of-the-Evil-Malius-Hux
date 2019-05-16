using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    public SpriteRenderer north;
    public SpriteRenderer south;
    public SpriteRenderer east;
    public SpriteRenderer west;
    public SpriteRenderer room;
    public SpriteRenderer backdrop;

    private List<SpriteRenderer> elements;

    // Start is called before the first frame update
    void Start()
    {
        //north.enabled = false;
        //south.enabled = false;
        //east.enabled = false;
        //west.enabled = false;
        //room.enabled = true;
        elements = new List<SpriteRenderer>
        {
            north, south, east, west, room, backdrop
        };
    }

    public void Hide()
    {
        room.enabled = false;
        north.enabled = false;
        south.enabled = false;
        east.enabled = false;
        west.enabled = false;
    }

    public void SetTransparency(float f)
    {
        Color c = new Color();
        foreach(SpriteRenderer sr in elements)
        {
            c = sr.color;
            c.a = f;
            sr.color = c;
        }
    }

    public void Match(Room r)
    {
        if (r.isActive())
        {
            //Debug.Log("Active Room");
            room.enabled = true;
            north.enabled = r.HasExit(Maze.Dir.N);
            south.enabled = r.HasExit(Maze.Dir.S);
            east.enabled = r.HasExit(Maze.Dir.E);
            west.enabled = r.HasExit(Maze.Dir.W);
        }
        else
        {
            room.enabled = false;
            north.enabled = false;
            south.enabled = false;
            east.enabled = false;
            west.enabled = false;
        }
    }

    public void setColour(Color c)
    {
        room.color = c;
    }


}
