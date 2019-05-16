using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityClusterOrb : ClusterOrb
{

    public float otherTimer;
    public float moveTime;
    public float proximityLimit;

    Transform player; 


    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(otherTimer > moveTime)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
        if(Vector3.Magnitude(transform.position - player.position) < proximityLimit)
        {
            explode();
        }
    }


    private void FixedUpdate()
    {
        base.FixedUpdate();
        otherTimer += Time.deltaTime;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

}
