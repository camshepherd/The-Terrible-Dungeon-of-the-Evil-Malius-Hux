using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShooter : Character
{
    public Transform player;
    public bool working;
    public GameObject missile;

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        working = true;
    }

    // Update is called once per frame
    public void Update()
    {
        if (working)
        {
            base.Update();
            fireMissile(missile);
        }
    }

    public void FixedUpdate()
    {
        if (working)
        {
            base.FixedUpdate();
            Vector3 target = player.position - transform.position;


            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            gameObject.GetComponent<Rigidbody2D>().SetRotation(angle);
        }
    }

}
