using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coloured : MonoBehaviour
{
    public Vector3 colourRGB;
    
    float flashTime = 0.1f;
    float flashTimer;
    bool flashing;

    protected void Update()
    {
        if (flashing)
        {
            if (flashTimer > flashTime)
            {
                flashing = false;
                updateDisplayColour();
            }
        }
    }

    public void setColour(Vector3 newColour)
    {
        colourRGB = newColour;
        updateDisplayColour();
    }

    public void updateDisplayColour()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(colourRGB.x, colourRGB.y, colourRGB.z);
    }

    public void updateDisplayColour(Vector3 newColour)
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(newColour.x, newColour.y, newColour.z);
    }

    public void flash()
    {
        // flash as the inverse colour
        //Vector3 newColour = new Vector3(255, 255, 255) - colourRGB;
        Vector3 newColour = new Vector3(1, 1, 1) - colourRGB;
        updateDisplayColour(newColour);
        flashTimer = 0;
        flashing = true;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    protected void FixedUpdate()
    {
        flashTimer += Time.deltaTime;
    }
}
