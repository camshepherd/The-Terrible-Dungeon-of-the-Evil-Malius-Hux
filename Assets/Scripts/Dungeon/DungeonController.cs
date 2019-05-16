using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    private Maze m;
    //public int length;
    public int size;

    public int colourRooms;

    public RoomManager roomPrefab;

    public ScreenScroll screenScroll;

    public EnemySpawner enemySpawner;

    public MapDisplay minimap;

    public SpecialCaseRooms scr;

    public bool spawnEnemies;

    private int startX;
    private int startY;

    public bool showMaze;

    private const int WIDTH = 16;
    private const int HEIGHT = 10;
    // Start is called before the first frame update
    void Start()
    {
        int length = GameStores.GetLength(); 
        // Create underlying maze.
        m = new Maze();
        m.Create(size);
        //m.MakeBoss(length);
        m.MakeRandomBoss();
        m.PopulateColourRooms(colourRooms);
        m.RemoveDeadEnds();
        m.AddCycles();
        m.SetRisk(length);
       
        startX = m.GetStart().getX();
        startY = m.GetStart().getY();

        BuildRoom(0, 0);
        screenScroll.SetOnMove(BuildRoom);

        if(showMaze)
            minimap.DisplayMaze(m);

        minimap.DrawRoom(m.GetStart());

    }

    public void BuildRoom(int x, int y)
    {
        //minimap.DisplayMaze(m);
        minimap.SetPlayerPosition(x+startX, y+startY);
        //Debug.Log((x + startX) + " " + (y + startY));
        Room r = m.GetRoom(x + startX, y + startY);
        if (!r.Visited())
        {
            GameObject g = Instantiate(roomPrefab.gameObject);
            g.transform.position = new Vector3(x * WIDTH, y * HEIGHT);
            RoomManager rm = g.GetComponent<RoomManager>();
            if(spawnEnemies)
                enemySpawner.SpawnShooters(r.getRisk());
            //rm.SetExits(m.GetRoom(x + startX, y + startY));
            r.Visit();
            rm.Set(r);
            scr.Check(r);
            minimap.DrawRoom(r);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
