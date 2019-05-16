using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBuilder : MonoBehaviour
{
    public HoverAndFade prefab;
    public float fadeTime;
    public float stayTime;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        TextRef.textBuilder = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateText(float x, float y, string text, Color fore, Color back)
    {
        HoverAndFade hf = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity).GetComponent<HoverAndFade>();
        hf.SetText(text);
        hf.SetColour(fore, back);
        hf.StartMe();
    }



    public void CreateText(Vector2 pos, string text)
    {
        CreateText(pos.x, pos.y, text, Color.black, Color.white);
    }

    public void CreateText(Vector2 pos, string text, Color fore, Color back)
    {
        CreateText(pos.x, pos.y, text, fore, back);
    }

}
