using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SimplePlayer : Character
{
    private Rigidbody2D myBody;
    public float baseSpeed;
    public float baseFireDelay;
    public float baseShieldModifier;
    public float speed;
    private Vector2 movement;
    private Weapon weapon;

    public GameObject missile;

    //public Vector3[] colours;
    private List<Vector3> colours;
    public int colourRef;
    public int coloursSize;

    public float colourSwitchTimer = 0;
    public float colourSwitchDelay = 0.5f;

    public float speedPowerUpTimer = 0;
    public float speedPowerUpEnd = 0;
    public float speedPowerUpRatio = 1;
    public bool speedBoosted = false;

    public float attackSpeedPowerUpTimer = 0;
    public float attackSpeedPowerUpEnd = 0;
    public float originalAttackDelay;
    public bool attackSpeedBoosted = false;

    public Canvas endCanvas;
    public Canvas healthCanvas;
    public List<GameObject> toDisableOnDeath;

    // Callbacks for the UI to use
    private System.Action<int> onColourSwitch;
    private System.Action<int> onNewColour;

    private float cheatTimer;
    private float cheatDelay = 1f;

    public void SetListeners(System.Action<int> onSwitch, System.Action<int> onNew)
    {
        onColourSwitch = onSwitch;
        onNewColour = onNew;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (endCanvas!=null)
            endCanvas.enabled = false;
        originalAttackDelay = fireDelay;
        colours = GameStores.GetPlayerColours();
        //colours[0] = colourRGB;
        coloursSize = 1;
        colourRef = 0;
        
        myBody = GetComponent<Rigidbody2D>();
        Debug.Log(myBody);
        movement = new Vector2();
        weapon = GetComponent<Weapon>();
        base.Start();
        shieldModifier = GameStores.GetDefMod();

        health = GameStores.GetHealth();
        onDamage?.Invoke();

        fireDelay = GameStores.GetAtkMod();


        //attackColourRGB = colourRGB;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dead)
        {
            base.FixedUpdate();
            speedPowerUpTimer += Time.deltaTime;
            colourSwitchTimer += Time.deltaTime;
            attackSpeedPowerUpTimer += Time.deltaTime;

            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");

            movement.Normalize();
            //myBody.AddForce(movement * speed);
            myBody.velocity = movement * speed * speedPowerUpRatio;

            Vector3 mousePos = Input.mousePosition;
            Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);

            mousePos.z = 0;
            mousePos.x = mousePos.x - playerPos.x;
            mousePos.y = mousePos.y - playerPos.y;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            //myBody.SetRotation(angle);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

            if (Input.GetAxis("Fire1") > 0)
            {
                fireMissile(missile);
            }

            if (Input.GetAxis("IncrementColour") > 0)
            {
                incrementColour();
            }
            else if(Input.GetAxis("DecrementColour") > 0)
            {
                decrementColour();
            }
            cheatTimer += Time.deltaTime;
        }
    }

    
    private void Update()
    {
        if(dead && !alreadyDead)
        {
            Die();
        }
        if (!dead)
        {
            base.Update();
            if(health < 0)
            {
                Die();
            }
            if (speedBoosted && speedPowerUpTimer > speedPowerUpEnd)
            {
                speedPowerUpRatio = 1;
                speedBoosted = false;
            }
            if (attackSpeedBoosted && attackSpeedPowerUpTimer > attackSpeedPowerUpEnd)
            {
                fireDelay = originalAttackDelay;
                attackSpeedBoosted = false;
            }
        }
        
        //Cheat mode
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.O) && cheatTimer > cheatDelay)
        {
            health += 900;
            healthMax += 900;
            flash();
            cheatTimer = 0;
        }
        if (Input.GetAxis("Cancel") > 0)
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }

    public void incrementColour()
    {
        if (colours.Count > 0 && colourSwitchTimer > colourSwitchDelay)
        {
            colourRef = (colourRef + 1) % colours.Count;
            setColour(colours[colourRef]);
            colourSwitchTimer = 0;
            onColourSwitch?.Invoke(colourRef);
        }

    }

    public void decrementColour()
    {
        if (colours.Count > 0 && colourSwitchTimer > colourSwitchDelay)
        {
            colourRef = (colourRef + colours.Count - 1) % colours.Count;
            setColour(colours[colourRef]);
            colourSwitchTimer = 0;
            onColourSwitch?.Invoke(colourRef);
        }

    }


    private void AdjustColourRef(Vector3 colour)
    {
        // Assume colour already exists in the array;
        colourRef = colours.IndexOf(colour);
        setColour(colours[colourRef]);
        onNewColour?.Invoke(colourRef);

    }
    
    public void addColour(Vector3 newColour)
    {
        bool foundColour = false;
        //for (int reff = 0; reff < coloursSize; reff++)
        //{
        //    //if (colours[reff].x == newColour.x && colours[reff].y == newColour.y && colours[reff].z == newColour.z)
        //    //{
        //    //    setAttackColour(colours[reff]);
        //    //    colourRef = reff;
        //    //    foundColour = true;
        //    //}

        //    // Set colour and current colour index to the picked up value
            
        //}

        if(colours.Contains(newColour))
        {
            setColour(newColour);
            colourRef = colours.IndexOf(newColour);
        }
        else
        {
            colours.Add(newColour);
            GameStores.AddColour(newColour);
            AdjustColourRef(newColour);
            Debug.Log("COL: " +colours.Count);
            //tb.CreateText(transform.position.x, transform.position.y, "+ NEW COLOUR");
            SpawnText("+ NEW COLOUR");
            //colours[coloursSize] = newColour;
            //coloursSize++;
            //incrementColour();
        }

    }
    
    public override void Die()
    {
        if (!alreadyDead)
        {
            Debug.Log("Player is dead!");
            myBody.velocity = new Vector2(0, 0);

            //GameObject.Find("TextHolder").SetActive(false);
            //GameObject.Find("GameOverCanvas").SetActive(true);
            endCanvas.enabled = true;
            
            alreadyDead = true;
            dead = true;

            foreach(GameObject x in toDisableOnDeath)
            {
                x.SetActive(false);
            }
        }
    }

    public void speedPowerUp(float duration, float power)
    {
        speedPowerUpTimer = 0;
        speedPowerUpEnd = duration;
        speedPowerUpRatio = power;
        speedBoosted = true;
    }


    public void attackSpeedPowerUp(float duration, float power)
    {
        originalAttackDelay = fireDelay;
        attackSpeedPowerUpEnd = duration;
        attackSpeedPowerUpTimer = 0;
        fireDelay = fireDelay / power;
        attackSpeedBoosted = true;
    }

    public void PermAttackSpeedPowerUp(float modifier)
    {
        fireDelay -= (baseFireDelay * modifier);
    }

    public void alterDefencePower(float modifier)
    {
        shieldModifier += modifier * baseShieldModifier;
    }
}
