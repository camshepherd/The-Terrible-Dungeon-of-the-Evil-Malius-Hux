using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCaseRooms : MonoBehaviour
{

    [System.Serializable]
    public struct ColourControls
    {
        public GameObject colourPrefab;
        //public Color defaultColour;
    }

    [System.Serializable]
    public struct EndRoomControls
    {
        public GameObject stairsPrefab;
    }

    public ColourControls colourControls;
    public EndRoomControls endRoomControls;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Check(Room r)
    {
        if (r.type == Room.Type.COLOUR)
            OnColourRoom(r);
        else if (r.type == Room.Type.BOSS)
            OnBossRoom(r);

    }

    public void OnColourRoom(Room r)
    {
        Vector3 center = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));

        List<Color> c = GameStores.AvailableColours();

        if (c.Count > 0)
        {
            GameObject g = Instantiate(colourControls.colourPrefab, new Vector3(center.x, center.y, 0), Quaternion.identity);
            ColourPickup cp = g.GetComponent<ColourPickup>();
            int randColor = Random.Range(0, c.Count);
            cp.SetColour(c[randColor]);
        }
    }

    private void OnBossRoom(Room r)
    {
        Vector3 center = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
        GameObject g = Instantiate(endRoomControls.stairsPrefab, new Vector3(center.x, center.y, 0), Quaternion.identity);
    }
}
