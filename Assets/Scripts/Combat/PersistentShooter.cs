using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentShooter : StaticShooter
{
    public float idealRange;
    
    public float runMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
        float enemyDistance = Vector3.Magnitude(transform.position - player.position);
        Vector3 runDirection = transform.position - player.position;
        if (enemyDistance < idealRange)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(runDirection * runMultiplier * idealRange / enemyDistance);
        }
        else if(enemyDistance > idealRange)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(-runDirection * runMultiplier * enemyDistance / idealRange);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
