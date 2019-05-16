using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoom : MonoBehaviour
{
    public GameObject walls;
    // Start is called before the first frame update
    void Start()
    {
        // X = 15, Y = 9
        GameObject g = Instantiate(walls, transform);
        g.transform.position = new Vector3(16, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
