using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using globals;

public class Health : MonoBehaviour
{
    Collider2D collider;
    public float healthMax, health;
    public bool dead, alreadyDead;
    public float shieldModifier = 1;

    // Start is called before the first frame update
    void Start()
    {
        health = healthMax;
        dead = false;
        alreadyDead = false;
        collider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    

}
