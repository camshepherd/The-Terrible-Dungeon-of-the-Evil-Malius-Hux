using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform bulletSpawn;
    public Transform pivot;

    public float shootDelay;

    private float cooldown;
    private System.Action act;

    private bool cooling = false;

    private bool shooting = false;

    private struct MyAct
    {
        public System.Action act;
        public float time;
    };

    private List<MyAct> actions;

    private Transform origin;
    private Transform target;
    private float movePercent;
    private float moveTime;
    private bool moving = false;

    // Scanning parameters
    private bool scanning = false;
    private Transform scanOrigin;
    private Transform scanTarget;
    private float scanPercentage;
    private float moveSpeed;
    private bool foundTarget = false;
    private Missile missile;
    private Transform player;

    private Color[] colours;
    private Color currentColour;

    // Start is called before the first frame update
    void Start()
    {
        actions = new List<MyAct>();
        colours = GameStores.gameColours;
    }



    // Update is called once per frame
    void Update()
    {
        //if (cooling)
        //{
        //    cooldown -= Time.deltaTime;
        //    if (cooldown <= 0)
        //    {
        //        act?.Invoke();
        //        cooling = false;
        //    }
        //}
        if(shooting)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0)
            {
                act?.Invoke();
                cooldown = shootDelay;
            }
        }

        if(moving)
        {
            movePercent += Time.deltaTime * (1 / moveTime);
            transform.position = Vector2.Lerp(origin.position, target.position, movePercent);
            transform.rotation = Quaternion.Lerp(origin.rotation, target.rotation, movePercent);
            if(movePercent>=1)
            {
                moving = false;
            }
        }

        if(scanning)
        {
            if (!foundTarget)
            {
                scanPercentage += Time.deltaTime * (1 / moveSpeed);
                Debug.Log(scanPercentage);
                transform.position = Vector2.Lerp(scanOrigin.position, scanTarget.position, scanPercentage);
                if(scanPercentage >= 1)
                {
                    Transform temp = scanOrigin;
                    scanOrigin = scanTarget;
                    scanTarget = temp;
                    scanPercentage = 0;
                }
                if(transform.position.y <= (player.transform.position.y + 0.5) &&
                    transform.position.y >= player.transform.position.y - 0.5)
                {
                    StartShooting(missile);
                    scanning = false;
                }
            }

            
        }

    }

    private void cooldownReset()
    {
        cooldown = shootDelay;
        cooling = true;
    }

    private void shoot(Missile m)
    {
        GameObject g = Instantiate(m.gameObject, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        g.GetComponent<SpriteRenderer>().color = currentColour;
        m.colourRGB = new Vector3(currentColour.r, currentColour.b, currentColour.g);
    }

    //public void Shoot(Missile m, int count)
    //{
    //    GameObject g = Instantiate(m.gameObject, bulletSpawn);
    //    count--;
    //    if (count > 0)
    //    {
    //        cooldownReset();
    //        act = () => Shoot(m, count);
    //    }

    //}

    public void StartShooting(Missile m)
    {
        currentColour = colours[Random.Range(0, colours.Length)];
        act = () => shoot(m);
        shooting = true;
    }

    public void StopShooting()
    {
        shooting = false;
    }

    public void MoveTo(Transform t, float time)
    {
        origin = transform;
        target = t;
        moveTime = time;
        movePercent = 0;
        moving = true;
    }

    public void StartScanning(Transform target, Transform player, float speed, Missile m)
    {
        scanOrigin = transform;
        scanTarget = target;
        scanPercentage = 0;
        foundTarget = false;
        moveSpeed = speed;
        missile = m;
        this.player = player;
        scanning = true;
    }

    public void Focus(Transform t)
    {
        Vector3 tv = t.position - pivot.position;
        float angle = Mathf.Atan2(tv.y, tv.x) * Mathf.Rad2Deg;
        pivot.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);

        // Fix rotation to face transform;
        //pivot.rotation = Quaternion.FromToRotation(pivot.position, t.position);

    }
}
