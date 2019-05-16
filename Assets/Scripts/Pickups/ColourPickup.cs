using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPickup : MonoBehaviour
{
    public Vector3 colourRGB;
    private SpriteRenderer sr;

    private bool activated = false;
    private AudioSource au; 

    // Start is called before the first frame update
    void Start()
    {
        ConfirmRenderer();
        au = GameObject.Find("AudioSource2").GetComponent<AudioSource>();
    }


    private void ConfirmRenderer()
    {
        if (!activated)
        {
            sr = gameObject.GetComponent<SpriteRenderer>();
            gameObject.GetComponent<SpriteRenderer>().color = new Color(colourRGB.x, colourRGB.y, colourRGB.z);
            activated = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    
    private void UpdateColour()
    {
        ConfirmRenderer();
        sr.color = new Color(colourRGB.x, colourRGB.y, colourRGB.z);
    }

    public void SetColour(Color c)
    {
        colourRGB = new Vector3(c.r, c.g, c.b);
        UpdateColour();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        bool foundColour = false;
        if(other.tag == "Player")
        {
            SimplePlayer player = other.GetComponent<SimplePlayer>();
            
            if (!foundColour)
            {
                player.addColour(colourRGB);
            }
            au.Play();
            Destroy(gameObject);
        }

    }

}
