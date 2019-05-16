using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
// attach to UI Text component (with the full text already there)

public class TypeWriter: MonoBehaviour 
{
    public float textDelay;
    public float lineDelay;

    Text txt;
    string story;
    
    void Awake()
    {
        txt = GetComponent<Text>();
        story = txt.text;
        txt.text = "";

        // TODO: add optional delay when to start
        StartCoroutine("PlayText");
    }

    IEnumerator PlayText()
    {
        foreach (char c in story)
        {
            if(c == '\n')
            {
                yield return new WaitForSeconds(lineDelay);
            }
            txt.text += c;
            yield return new WaitForSeconds(textDelay);
        }
    }

    IEnumerator wait(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
    

}