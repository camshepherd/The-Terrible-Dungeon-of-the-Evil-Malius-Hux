using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class RoomManager : MonoBehaviour
{
    public Tilemap walls;
    public Tile door;

    public SpriteRenderer indicator;

    [System.Serializable]
    public struct TypeColour{
        public Room.Type type;
        public Color colour;
    }

    public TypeColour[] typeColours;

    public GameObject[] obstructions;
    public GameObject[] scenery;

    private Dictionary<Room.Type, Color> typeDict;

    private const int dx = 8;
    private const int dy = 5;

    private bool dictBuilt = false;
    
    // Start is called before the first frame update
    void Start()
    {
        checkDict();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(Room r)
    {
        SetExits(r);
        SetIndicator(r);

    }

    private void checkDict()
    {
        if (!dictBuilt)
        {
            typeDict = new Dictionary<Room.Type, Color>();
            foreach (TypeColour tc in typeColours)
            {
                typeDict.Add(tc.type, tc.colour);
            }
            dictBuilt = true;
        }
    }

    private void SetIndicator(Room r)
    {
        checkDict();
        if(typeDict.ContainsKey(r.type))
        {
            indicator.color = typeDict[r.type];
        } else
        {
            indicator.gameObject.SetActive(false);
        }
    }

    public void SetExits(Room r)
    {
        if(!r.HasExit(Maze.Dir.N)) { walls.SetTile(new Vector3Int(0, dy, 0), door); }
        if (!r.HasExit(Maze.Dir.S)) { walls.SetTile(new Vector3Int(0, -dy, 0), door); }
        if (!r.HasExit(Maze.Dir.E)) { walls.SetTile(new Vector3Int(dx, 0, 0), door); }
        if (!r.HasExit(Maze.Dir.W)) { walls.SetTile(new Vector3Int(-dx, 0, 0), door); }
    }
}
