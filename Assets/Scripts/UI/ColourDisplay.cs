using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourDisplay : MonoBehaviour
{
    public GameObject pointer;
    public SpriteRenderer[] colours;

    public void OnChangeColour(int choice)
    {
        //Vector2 pos = pointer.transform.position;
        //pos.x = choice;
        pointer.transform.position = colours[choice].gameObject.transform.position;
    }

    public void OnAddColour(int choice)
    {
        List<Color> currentCol = GameStores.currentColours;
        for(int i = 0; i<colours.Length; i++)
        {
            if (i < currentCol.Count)
                colours[i].color = currentCol[i];
            else
                colours[i].color = Color.clear;
        }
        OnChangeColour(choice);
    }
    // Start is called before the first frame update
    void Start()
    {
        // Use callbacks to add them to the player
        SimplePlayer sp = GameObject.FindGameObjectWithTag("Player").GetComponent<SimplePlayer>();
        sp.SetListeners(OnChangeColour, OnAddColour);
        OnAddColour(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
