using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackPower;
    public float timer;
    public float attackDelay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && timer > attackDelay)
        {
            collision.gameObject.GetComponent<SimplePlayer>().takeDamage(attackPower, gameObject.GetComponent<Coloured>().colourRGB);
            timer = 0;
        }
    }
}
