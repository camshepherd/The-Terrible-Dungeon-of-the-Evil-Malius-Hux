using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBolt : Missile
{
    public float chaseQuality;
    GameObject target;
    private float duration;

    private float homingLifetime = 5;
    private float homingtimer;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        

        if(homingtimer < homingLifetime)
        {
            Vector3 targetPos = target.transform.position;
            Vector3 pos = transform.position;
            //myBody.SetRotation(angle);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector3 convertedVelocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            rb.velocity = (((targetPos - pos).normalized * chaseQuality) + convertedVelocity).normalized * speed;
        }
        
        
    }



    private void FixedUpdate()
    {
        base.FixedUpdate();
        homingtimer += Time.deltaTime;
    }
}
