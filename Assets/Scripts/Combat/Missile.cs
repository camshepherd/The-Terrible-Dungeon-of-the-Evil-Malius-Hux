using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Coloured
{
    public float speed = 1;
    public float damage;


    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        //gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        //print("Transform.forward = " + transform.forward);
        //print("speed + " + speed);
    }

    protected void FixedUpdate() {
        base.FixedUpdate();
        
    }

    protected void Update()
    {
        base.Update();
        Vector3 loc = Camera.main.WorldToViewportPoint(transform.position);
        if (loc.x < 0 || loc.x > 1 || loc.y < 0 || loc.y > 1)
        {
            Destroy(this.gameObject);
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Missile" && other.tag != "PickUp")
        {
            Destroy(this.gameObject);
        }
    }
}
