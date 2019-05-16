using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image image;
    public float fadeOutSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image.color += new Color(0, 0, 0, fadeOutSpeed * Time.deltaTime);
    }
}
