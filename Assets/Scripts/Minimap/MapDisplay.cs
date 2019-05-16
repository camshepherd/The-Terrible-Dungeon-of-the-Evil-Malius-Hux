using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Transform parent;
    public GameObject roomCell;
    public int size;
    //public float width = 1f;
    //public float height = 1f;
    private Maze m;

    public Color defaultColour;

    private bool init = false;

    public Transform centre;

    private int radius = 3;

    [System.Serializable]
    public struct TypeColour
    {
        public Room.Type type;
        public Color colour;
    }

    public TypeColour[] typeColours;

    private Dictionary<Room.Type, Color> typeDict;

    public GameObject playerIcon;
    private TransparencyController tc;

    private GameObject player;

    public bool allVisible;
    

    private RoomController[,] cells;
    // Start is called before the first frame update
    void Start()
    {
        tc = playerIcon.GetComponent<TransparencyController>();
        //PopulateColours();
        //DrawCells();
        initMe(null);
        // This should be removed in the future
        //m = new Maze();
        //m.Create(5);
        //m.MakeBoss(3);
        //m.RemoveDeadEnds();
        //DisplayMaze(m);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void PopulateColours()
    {
        typeDict = new Dictionary<Room.Type, Color>();
        foreach (TypeColour tc in typeColours)
        {
            typeDict.Add(tc.type, tc.colour);
        }
    }

    private void initMe(System.Action action)
    {
        if(!init)
        {
            cells = new RoomController[size, size];
            PopulateColours();
            DrawCells();
            init = true;
        }
        action?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        // Change transparency based off of player proximity
        float distance = Vector2.Distance(centre.position, player.transform.position);

        if(distance<radius)
        {
            SetTransparency(distance / radius);
        }
    }

    private void DrawCells()
    {
        Vector2 position = Vector2.zero;
        // Need to instantiate and position the cells
        for(int i = 0; i < size; i++)
        {
            for(int j=0; j< size; j++)
            {
                position.x = transform.localScale.x * j;
                position.y = transform.localScale.y * i;
                //GameObject g = Instantiate(roomCell, position, Quaternion.identity);
                GameObject g = Instantiate(roomCell, transform, false);
                g.transform.Translate(position);
                cells[i, j] = g.GetComponent<RoomController>();
                cells[i, j].Hide();
            }
        }
    }

    public void DisplayMaze(Maze m)
    {
        if(init)
        {
            DrawMaze(m);
        } else
        {
            initMe(() => DrawMaze(m));
        }
    }

    private void DrawMaze(Maze m)
    {
        //initMe();
        // Display each room in the maze
        foreach(Room r in m.GetRooms())
        {
            //Debug.Log(r.GetExits());
            DrawRoom(r);
        }

    }

    public void SetTransparency(float f)
    {
        tc.SetTransparency(f);
        foreach(RoomController rc in cells)
        {
            rc.SetTransparency(f);
        }
        
    }

    public void DrawRoom(Room r)
    {
        RoomController rc = cells[r.getY(), r.getX()];
        rc.Match(r);
        if (typeDict.ContainsKey(r.type))
            rc.setColour(typeDict[r.type]);
        else
            rc.setColour(defaultColour);
    }

    public void SetPlayerPosition(int x, int y)
    {
        //Debug.Log(playerIcon);
        if(init)
        {
            playerIcon.transform.position = cells[y, x].gameObject.transform.position;
        }
        else
        {
            //Debug.Log(cells[y, x] == null);
            initMe(() => SetPlayerPosition(x, y));
        }
    }
}
