using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 16:9 screen size translates to 18:10 cells per page. But may need a more intelligent measure

public class CreateTiles : MonoBehaviour
{
    public Tilemap tm;
    public Tile fill;
    public Tile empty;
    private Vector3Int pos;
    // Start is called before the first frame update
    void Start()
    {
        tm.SetTile(Vector3Int.zero, fill);
        tm.SetTile(new Vector3Int(0, 1, 0), fill);
        pos = new Vector3Int();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BinTree()
    {

    }

    public Vector3Int GetTilePos(Vector3 position)
    {
        return tm.WorldToCell(position);
    }

    private void assignPos(int x, int y)
    {
        pos.x = x; pos.y = y;
    }

    public void Fill(int x, int y)
    {
        assignPos(x, y);
        tm.SetTile(pos, fill);
    }

    public void Empty(int x, int y)
    {
        assignPos(x, y);
        tm.SetTile(pos, empty);
    }
}
