using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterOrb : Missile
{

    public GameObject missileTemplate;
    public Transform[] spawnPoints;

    public float timer = 0;
    public float lifeTime;

    public bool hasTimer = false;

    private float secondTimer;
    private float minimumTimeToExplode = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    public void Update()
    {
        base.Update();
        if(timer > lifeTime)
        {
            explode();
        }
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        base.FixedUpdate();
        if (hasTimer)
        {
            timer += Time.deltaTime;
        }
        secondTimer += Time.deltaTime;
    }

    public void explode()
    {
        if(secondTimer > minimumTimeToExplode)
        {
            for (int x = 0; x < spawnPoints.Length; x++)
            {
                GameObject theMissile = Instantiate(missileTemplate, spawnPoints[x].position, spawnPoints[x].rotation);
                theMissile.GetComponent<Missile>().speed = 6.0f;
                theMissile.GetComponent<Coloured>().setColour(colourRGB);
            }
            Destroy(gameObject);
        }
        
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Missile" && other.tag != "PickUp")
        {
            explode();
        }
    }

}
