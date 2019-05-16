using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using globals;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float damageModifier = 1.1f;
    public float attackDelay;
    public float attackPersistence;
    public float persistTimer;
    public Vector3 colourRGB;
    public bool attacking = false;

    public bool colliderPresent = false;
    public float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking && timer > attackPersistence)
        {
            attacking = false;
        }
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
    }

    public void attack()
    {
        if(timer >= attackDelay)
        {
            if (colliderPresent)
            {
                Destroy(gameObject.GetComponent<CircleCollider2D>());
                colliderPresent = false;
            }
            //spawn collider, do damage
            timer = 0;
            attacking = true;
            CircleCollider2D circle = gameObject.AddComponent<CircleCollider2D>();
            gameObject.GetComponent<CircleCollider2D>().radius = damageModifier;
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            colliderPresent = true;
        }
    }

    public void changeColour(Vector3 newColour)
    {
        colourRGB = newColour;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(colourRGB.x, colourRGB.y, colourRGB.z);
    }
}
