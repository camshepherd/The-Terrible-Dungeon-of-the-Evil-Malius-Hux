using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Vector2 movement;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        movement = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        movement.Normalize();
        //myBody.AddForce(movement * speed);
        myBody.velocity = movement * speed;
    }
}
