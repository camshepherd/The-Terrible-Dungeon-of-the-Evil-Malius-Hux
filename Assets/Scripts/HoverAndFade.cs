using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverAndFade : MonoBehaviour
{
    public Text text;
    public float speed;
    public float staticTime;
    private float currentStatic;
    public float fadeTime;
    private float currentFade;

    private Vector3 position;

    public Outline outline;

    private float currentDistance;
    private Color textColour;

    public bool fading = false;
    private bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.transform.position;
        currentStatic = staticTime;
        currentFade = fadeTime;
        textColour = text.color;
    }

    public void SetText(string display)
    {
        text.text = display;
    }

    public void SetColour(Color fore, Color back)
    {
        text.color = fore;
        outline.effectColor = back;
    }

    public void StartMe() { started = true; }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            // Move up
            gameObject.transform.Translate(0, speed * Time.deltaTime, 0);

            if (!fading)
            {
                // Reduce time
                currentStatic -= Time.deltaTime;
                if (currentStatic <= 0)
                    fading = true;
            }
            else
            {
                // Reduce time
                currentFade -= Time.deltaTime;
                textColour.a = Mathf.Lerp(0, 1, currentFade / fadeTime);
                text.color = textColour;
                if (currentFade <= 0)
                    Destroy(gameObject);
            }
        }
    }
}
