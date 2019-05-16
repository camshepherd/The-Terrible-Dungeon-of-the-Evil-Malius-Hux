using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashText : MonoBehaviour
{
    public float timePresent;
    public float timeGone;
    public bool present = true;
    public float timer;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
    }

    private void Update()
    {
        if (present && timer > timePresent)
        {
            text.enabled = false;
            present = false;
            timer = 0;
        }
        else if(!present && timer > timeGone)
        {
            text.enabled = true;
            present = true;
            timer = 0;
        }
    }
}
