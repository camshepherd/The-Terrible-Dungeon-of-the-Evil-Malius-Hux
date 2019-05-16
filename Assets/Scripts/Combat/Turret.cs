using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Character
{
    public bool active = true;
    public GameObject missile;

    public float phase = 0f;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        fireTimer = phase;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            base.Update();
            fireMissile(missile);
        }
    }
}
