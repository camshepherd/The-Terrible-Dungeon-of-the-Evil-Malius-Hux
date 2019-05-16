using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveOnNoFocus : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 coords = Camera.main.WorldToViewportPoint(transform.position);
        if(coords.x < 0 || coords.x > 1 || coords.y < 0 || coords.y > 1)
        {
            gameObject.GetComponent<StaticShooter>().working = false;
        }
        else
        {
            gameObject.GetComponent<StaticShooter>().working = true;
        }
    }
}
