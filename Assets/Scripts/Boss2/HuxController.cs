using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HuxController : Character
{
    public float delay;
    private float currentDelay;

    public List<Vector3> ownColours;

    public float movementDelay;
    public float movementSpeed;

    public float attackTimer;
    public float[] attackDelays;

    public List<GameObject> missiles;
    public int missileRef;

    public List<int> colourOrder;

    public int state;
    public bool moving;
    public float moveTimer;
    public float moveDelay;

    public float colourTimer;
    public float colourDelay;
    public int colourRef;

    public Vector3[] locations;
    public int locationRef;

    public float speed;
    public float randomness;

    public float missileTimer;
    public float missileDelay;

    public int stage;

    float angrySoundTimer;
    float angrySoundDelay;

    public float stunDamage;
    public float stunTime;
    public float stunTimer;
    public float stunRotationSpeed;
    public float stunColourTimer;
    public float stunColourDelay;
    public bool stunned = false;

    private Transform player;
    private Rigidbody2D rb;

    public AudioClip angrySound;
    public AudioSource otherAudioPlayer;

    private bool alreadyStarted;
    public bool shootingPaused;
    public float restPeriodTimer;
    public float restDuration;

    int colourOrderRef;

    public AudioSource globalMusicPlayer;

    public AudioClip stunSound;

    public float initialDelay;
    // Start is called before the first frame update
    void Start()
    {
        alreadyStarted = false;
        setColour(new Vector3(0, 0, 0));
        StartCoroutine(DoStartStuff());
    }

    public void Start2()
    {
        alreadyStarted = true;
        base.Start();
        setColour(new Vector3(0, 0, 0));
        currentDelay = delay;
        state = 0;
        moving = true;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        randomness = 0.1f;
        speed = 0.1f;
        colourOrderRef = 0;
        goToRightState();
        //Tilemap tileMap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();
        //tileMap.SetTile(new Vector3Int(2, 4, 0), null);
        //tileMap.SetTile(new Vector3Int(1, 4, 0), null);
        //StageThree();
    }

    public void FixedUpdate()
    {
        base.FixedUpdate();
        if (moving)
        {
            Vector3 direction = locations[locationRef] - transform.position;
            float distance = Vector3.Magnitude(locations[locationRef] - transform.position);
            Vector2 changeInPos = ((new Vector2(direction.x, direction.y)).normalized + new Vector2(Random.Range(-randomness,randomness),Random.Range(-randomness,randomness))) * speed;
            transform.position += new Vector3(changeInPos.x, changeInPos.y, 0);

            Vector3 target = player.position - transform.position;


            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            gameObject.GetComponent<Rigidbody2D>().SetRotation(angle);
            colourTimer += Time.deltaTime;
            missileTimer += Time.deltaTime;
        }
        if (stunned)
        {
            stunColourTimer += Time.deltaTime;
            stunTimer += Time.deltaTime;
        }
        if (shootingPaused)
        {
            restPeriodTimer += Time.deltaTime;
        }
        angrySoundTimer += Time.deltaTime;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !alreadyStarted)
        {
            
            Start2();
        }
        base.Update();
        
        attackTimer += Time.deltaTime;
        moveTimer += Time.deltaTime;
        if (moving)
        {
            goToRightState();
            if (moveTimer > moveDelay)
            {
                locationRef = (locationRef + 1) % locations.Length;
                moveTimer = 0;
            }
            if (!shootingPaused)
            {
                if (attackTimer > fireDelay)
                {
                    attackTimer = 0;
                    fireMissile(missiles[missileRef]);
                }
                if (colourTimer > colourDelay)
                {
                    colourOrderRef = (colourOrderRef + 1) % colourOrder.Capacity;
                    colourRef = colourOrder[colourOrderRef];
                    changeAttackColour(ownColours[colourRef]);
                    colourTimer = 0;
                    
                    shootingPaused = true;
                    restPeriodTimer = 0;
                }
                if (missileTimer > missileDelay)
                {
                    missileTimer = 0;
                    missileRef = (missileRef + 1) % 4;

                }
            }
            else if (shootingPaused)
            {
                if(restPeriodTimer > restDuration)
                {
                    shootingPaused = false;
                    moving = true;
                }
            }
            if(stage == 2)
            {
                if(angrySoundTimer > angrySoundDelay)
                {
                    angrySoundTimer = 0;
                    otherAudioPlayer.Play();
                    angrySoundDelay = Random.Range(3, 4);
                }
            }
            else if(stage == 3)
            {
                if(angrySoundTimer > angrySoundDelay)
                {
                    angrySoundTimer = 0;
                    otherAudioPlayer.Play();
                    angrySoundDelay = Random.Range(1, 2.5f);
                }
            }
        }

        else if (stunned)
        {
            if(stunColourTimer > stunColourDelay)
            {
                setColour(new Vector3(Random.Range(0.2f, 1), Random.Range(0.2f, 1), Random.Range(0.2f, 1)));
                stunColourTimer = 0;
            }
            if(stunTimer > stunTime)
            {
                stunned = false;
                state += 1;
                moving = true;
                goToRightState();
            }
        }
        if (!stunned && au.clip == stunSound)
        {
            au.Stop();
        }
    }

    public void goToRightState()
    {
        if(stage != 1 && health/healthMax > (0.66))
        {
            StageOne();
        }
        else if(stage < 2 && health/healthMax < 0.66)
        {
            StageTwo();
        }
        else if(stage < 3 && health/healthMax < 0.33)
        {
            StageThree();
        }
        
    }


    public void StageOne()
    {
        stage = 1;
        speed = 0.08f;
        randomness = 0.07f;
        fireDelay = 0.2f;
        moving = true;
        stunned = false;
        colourDelay = 2.8f;
        RandomColourOrder(2);
        setColour(new Vector3(0,0,0));
        colourRef = 0;
        missileDelay = 1;
        stunTime = 4;
        shootingPaused = false;
        restDuration = 1;
        //Stun();
    }

    public void StageTwo()
    {
        stage = 2;
        speed = 0.12f;
        randomness = 0.12f;
        moving = true;
        stunned = false;
        colourDelay = 1.8f;
        RandomColourOrder(4);
        setColour(new Vector3(0, 0, 0));
        colourRef = 0;
        missileDelay = 0.7f;
        stunTime = 4;
        shootingPaused = false;
        restDuration = 0.8f;
        nextNoise *= 0.8f;
        otherAudioPlayer.clip = angrySound;
    }

    public void StageThree()
    {
        stage = 3;
        speed = 0.18f;
        movementDelay = 1.2f;
        randomness = 0.3f;
        moving = true;
        stunned = false;
        colourDelay = 1.8f;
        RandomColourOrder(6);
        setColour(new Vector3(0, 0, 0));
        colourRef = 0;
        missileDelay = 0.7f;
        stunTime = 4;
        restDuration = 0.3f;
        nextNoise *= 0.7f;
        otherAudioPlayer.clip = angrySound;
        otherAudioPlayer.Play();
    }
    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Missile" && collision.GetComponent<HomingBolt>() != null)
        {
            Vector3 attackColour= collision.gameObject.GetComponent<Missile>().colourRGB;
            if (attackColour == new Vector3(0, 0, 0) && !stunned)
            {
                Stun();
            }
        }
        else
        {
            base.OnTriggerEnter2D(collision);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        print("Stuff happens!");
    }

    public void RandomColourOrder(int numColours)
    {
        colourOrder = new List<int> { 0 };
        List<int> order = new List<int>{ 0,1,2,3,4,5,6};
        for(int x = 0; x < numColours; x++)
        {
            int nextToAdd = order[Random.Range(1, 6 - x)];
            colourOrder.Add(nextToAdd);
            order.Remove(nextToAdd);
        }
        Debug.Log("changed colours!");
    }

    public void Stun()
    {
        moving = false;
        stunTimer = 0;
        rb.velocity = new Vector2(0,0);
        rb.angularVelocity = 16;
        stunColourTimer = 0;
        stunned = true;
        au.Stop();
        au.clip = stunSound;
        au.Play();
    }

    public override void Die()
    {
        onDeath?.Invoke();
        Tilemap tileMap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();
        tileMap.SetTile(new Vector3Int(2, 4, 0), null);
        tileMap.SetTile(new Vector3Int(1, 4, 0), null);

        globalMusicPlayer.Stop();
        Destroy(gameObject);


    }

    public IEnumerator DoStartStuff()
    {
        yield return new WaitForSeconds(initialDelay);
        if (!alreadyStarted)
        {
            Start2();
        }
    }

}
