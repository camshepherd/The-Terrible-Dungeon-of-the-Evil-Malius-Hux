using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Coloured
{
    public Vector3 attackColourRGB;
    private float fireFlashTimer = 0;
    private float fireFlashDelay = 0.1f;
    private bool fireFlashing = false;

    public float fireDelay = 0.1f;
    public float fireTimer;

    public Transform firePoint;


    public float noiseTimer;
    public float nextNoise;
    

    public AudioClip ambientSound;
    public float healthMax;
    public float health;
    public bool shieldActive = false;
    public float shieldModifier = 1;

    public bool dead;
    public bool alreadyDead;

    public GameObject[] dropOnDeath;
    public float dropChance = 0.5f;

    private TextBuilder tb;
    protected AudioSource au;
    // On damage callback for health bars
    protected System.Action onDamage;
    protected System.Action onDeath;

    public void SetDamageCallback(System.Action damage)
    {
        onDamage = damage;
        onDamage();
    }

    public void SetDeathCallback(System.Action onDeath)
    {
        this.onDeath = onDeath;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        health = healthMax;
        fireFlashTimer = 0;
        fireFlashing = false;
        dead = false;
        alreadyDead = false;
        if (attackColourRGB == new Vector3(0, 0, 0))
        {
            attackColourRGB = colourRGB;
        }
        au = GetComponent<AudioSource>();
        au.clip = ambientSound;
        tb = GameObject.FindGameObjectWithTag("TextBuilder").GetComponent<TextBuilder>();
        
    }

    protected void Update()
    {
        base.Update();
        if(fireFlashing && fireFlashTimer > fireFlashDelay)
        {
            fireFlashing = false;
            updateDisplayColour();
        }
        if(noiseTimer > nextNoise && !au.isPlaying)
        {
            au.Play();
            noiseTimer = 0;
            nextNoise = Random.Range(0, 0.2f);
        }
    }

    protected void SpawnText(string text)
    {
        tb.CreateText(transform.position, text);
    }

    private Vector3 avg(Vector3 vector)
    {
        float sum = vector.x + vector.y + vector.z;
        Vector3 output = new Vector3(vector.x / sum, vector.y / sum, vector.z / sum);

        return output;
    }

    private float calculateDamage(float magnitude, Vector3 weapon, Vector3 shield, float shieldMod)
    {
        Debug.Log(weapon);
        if(weapon.Equals(new Vector3(255.0f,255.0f,255.0f)))
        {
            return magnitude;
        }
        Vector3 defRatio = avg(shield);
        Vector3 atkRatio = avg(weapon);
        Vector3 res = atkRatio - defRatio;
        float result = 0;
        if (res.x > 0) result += res.x;
        if (res.y > 0) result += res.y;
        if (res.z > 0) result += res.z;

        //return (result * magnitude) + (1 - result) * magnitude * (1 / shieldMod);
        float denom = (shieldMod * (1 - result) * 2);
        denom = denom == 0 ? 1 : denom;
        return magnitude * (1/denom);

        //atkRatio = atkRatio. (defRatio * (1 / shieldMod));
    }

    public void takeDamage(float magnitude, Vector3 weaponColour)
    {
        //Debug.Log("BEF: " + weaponColour);
        //weaponColour = weaponColour.normalized;
        //Debug.Log("Test: " + weaponColour);
        //Debug.Log("Mag: " + magnitude);
        //Vector3 attackResult = 
        //Vector3 attackResult = (weaponColour * magnitude) - (GetComponent<Coloured>().colourRGB.normalized * shieldModifier);
        //Debug.Log("ATK:" + attackResult);
        //if (attackResult.x < 0)
        //{
        //    attackResult.x = Mathf.Clamp(attackResult.x, 0, 255);
        //}
        //if (attackResult.y < 0)
        //{
        //    attackResult.y = Mathf.Clamp(attackResult.y, 0, 255);
        //}
        //if (attackResult.z < 0)
        //{
        //    attackResult.z = Mathf.Clamp(attackResult.z, 0, 255);
        //}

        //if (shieldActive)
        //{

        //}

        ////Never do less than 1 damage.
        //float damage = Mathf.Max((attackResult.x + attackResult.y + attackResult.z), 1);
        
        
        float damage = calculateDamage(magnitude, weaponColour, GetComponent<Coloured>().colourRGB, shieldModifier);
        if (colourRGB == new Vector3(0, 0, 0))
        {
            damage = 0;
        }
        health -= damage;

        //Debug.Log(damage);

        SpawnText("-" + string.Format("{0, 0:F1}", damage));


        if (health < 0)
        {
            dead = true;
            Die();
        }

        flash();
        onDamage?.Invoke();
    }

    // Virtual to enable overriding
    public virtual void Die()
    {
        onDeath?.Invoke();
        if(dropOnDeath.Length > 0)
        {
            float chance = Random.Range(0f, 1f);
            if(chance<=dropChance)
            {
                int choice = Random.Range(0, dropOnDeath.Length);
                Instantiate(dropOnDeath[choice], transform.position, Quaternion.identity);
            }
        }
        //Play some animation.

        Destroy(gameObject);
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        base.FixedUpdate();
        fireFlashTimer += Time.deltaTime;
        fireTimer += Time.deltaTime;
        if (!au.isPlaying)
        {
            noiseTimer += Time.deltaTime;
        }
    }

    public void fireMissile(GameObject missile)
    {
        if (fireTimer > fireDelay)
        {
            GameObject theMissile = Instantiate(missile, firePoint.position,firePoint.rotation);
            //theMissile.GetComponent<Missile>().speed = 6.0f;
            theMissile.GetComponent<Coloured>().setColour(attackColourRGB);
            fireFlashing = true;
            fireFlashTimer = 0;
            fireTimer = 0;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Missile")
        {
            takeDamage(other.GetComponent<Missile>().damage, other.GetComponent<Missile>().colourRGB);
        }
        if (other.gameObject.GetComponent<Weapon>() != null && other.gameObject.GetComponent<Weapon>().attacking)
        {
            Weapon enemyWeapon = other.gameObject.GetComponent<Weapon>();
            takeDamage(enemyWeapon.damage * enemyWeapon.damageModifier, enemyWeapon.colourRGB);
            gameObject.GetComponent<Character>().flash();
        }
    }


    public void setAttackColour(Vector3 newColour)
    {
        attackColourRGB = newColour;
    }

    public void changeAttackColour(Vector3 newColour)
    {
        attackColourRGB = newColour;
        flash();
    }

    public void healDamage(float restoreAmount)
    {
        tb.CreateText(transform.position, "+" + restoreAmount.ToString(), Color.green, Color.black);
        health += restoreAmount;
        if(health > healthMax)
        {
            health = healthMax;
        }
        onDamage();
    }
}
