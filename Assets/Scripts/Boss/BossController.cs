using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GunController smallLeft;
    public GunController smallRight;
    public GunController bigLeft;
    public GunController bigRight;

    public Missile simple;
    public Missile cluster;
    public Missile orb;

    public Transform topLeft;
    public Transform topRight;
    public Transform bottomLeft;
    public Transform bottomRight;
    public Transform originLeft;
    public Transform originRight;

    private SimplePlayer sp;

    public bool bossEncounter = true;

    private System.Action act;
    private System.Action next;
    private float actTime;
    private bool acting = false;

    public float delay;
    private float currentDelay;

    public float scanSpeed;

    public float p1time;
    public float p2time;
    public float p3time;
    public float p4time;
    public float p5time;

    public AudioClip[] audioClips;
    private AudioSource au;

    public Character healthController;
    public GameObject displayOnDeath;
    // Start is called before the first frame update
    void Start()
    {
        healthController.SetDeathCallback(Die);
        sp = GameObject.FindGameObjectWithTag("Player").GetComponent<SimplePlayer>();
        //SetDelay(PhaseOne);
        SetAction(null, delay, PhaseOneStart);
        au = GetComponent<AudioSource>();
    }

    private void Die()
    {
        Transition();
        bossEncounter = false;
        displayOnDeath.SetActive(true);
        GameObject.Find("Audio Source").GetComponent<AudioSource>().Stop();
    }

    private void DelayThen(System.Action onDone)
    {
        SetAction(null, delay, onDone);
    }

    private void WaitThen(float time, System.Action then)
    {
        SetAction(null, time, then);
    }

    private void SetAction(System.Action action, float time, System.Action then)
    {
        actTime = time;
        currentDelay = delay;
        act = action;
        next = then;
        acting = true;
    }

    private void QueueNext(System.Action next, float time)
    {
        acting = true;
    }

    // Update is called once per frame
    void Update()
    {
        //smallLeft.Focus(t);
        //smallRight.Focus(t);
        if (bossEncounter)
        {
            if (acting)
            {
                act?.Invoke();
                actTime -= Time.deltaTime;
                if (actTime <= 0)
                {
                    acting = false;
                    next?.Invoke();
                }
            }
        }
        //else
        //{
        //    delay -= Time.deltaTime;
        //    if(delay<=0)
        //    {
        //        done?.Invoke();
        //    }
        //}

    }

    private void PhaseOneStart()
    {
        au.clip = audioClips[0];
        au.Play();
        // Point small guns at player and shoot
        Transform t = sp.gameObject.transform;
        smallLeft.Focus(t);
        smallRight.Focus(t);
        smallLeft.StartShooting(simple);
        smallRight.StartShooting(simple);
        SetAction(() =>
        {
            smallLeft.Focus(t);
            smallRight.Focus(t);
        }, p1time, () =>
        {
            Transition();
            DelayThen(PhaseTwo);
        });

    }

    private void Transition()
    {
        au.clip = audioClips[5];
        au.Play();
        // Stop all shooting
        smallLeft.StopShooting();
        smallRight.StopShooting();
        bigLeft.StopShooting();
        bigRight.StopShooting();
    }

    private void PhaseTwo()
    {
        au.clip = audioClips[1];
        au.Play();
        // Shoot cluster bullets from large guns
        bigLeft.shootDelay = 1;
        bigRight.shootDelay = 1;
        bigRight.StartShooting(cluster);
        bigLeft.StartShooting(cluster);

        SetAction(null, p2time,
            () =>
            {
                Transition();
                DelayThen(PhaseThree);
            });
    }

    private void PhaseThree()
    {
        au.clip = audioClips[2];
        au.Play();
        // Move large guns to the side.
        // Shoot orbs when the player at close y axis
        float moveTime = 1;
        bigLeft.MoveTo(topLeft, moveTime);
        bigRight.MoveTo(bottomRight, moveTime);
        bigLeft.shootDelay = 0.5f;
        bigRight.shootDelay = 0.5f;

        WaitThen(moveTime, () => {
            bigLeft.StartScanning(bottomLeft, sp.gameObject.transform, scanSpeed, orb);
            bigRight.StartScanning(topRight, sp.gameObject.transform, scanSpeed, orb);
            WaitThen(p3time, () =>
            {
                Transition();
                bigLeft.MoveTo(originLeft, moveTime);
                bigRight.MoveTo(originRight, moveTime);
                DelayThen(PhaseFour);

            });
        });
    }


    private void PhaseFour()
    {
        au.clip = audioClips[3];
        au.Play();
        //Debug.Log("P4");
        // Combine phase one and two
        Transform t = sp.gameObject.transform;
        smallLeft.StartShooting(simple);
        smallRight.StartShooting(simple);
        bigRight.shootDelay = 1;
        bigLeft.shootDelay = 1;
        bigLeft.StartShooting(cluster);
        bigRight.StartShooting(cluster);

        SetAction(() =>
        {
            smallLeft.Focus(t);
            smallRight.Focus(t);
        }, p4time, () =>
        {
            Transition();
            DelayThen(PhaseFive);
        });
    }

    private void PhaseFive()
    {
        au.clip = audioClips[4];
        au.Play();
        //Debug.Log("P5");
        // Combine phase one and three
        Transform t = sp.gameObject.transform;
        smallLeft.StartShooting(simple);
        smallRight.StartShooting(simple);

        float moveTime = 1;
        bigLeft.MoveTo(topLeft, moveTime);
        bigRight.MoveTo(bottomRight, moveTime);
        bigLeft.shootDelay = 0.5f;
        bigRight.shootDelay = 0.5f;


        WaitThen(moveTime, () => {
            bigLeft.StartScanning(bottomLeft, sp.gameObject.transform, scanSpeed, orb);
            bigRight.StartScanning(topRight, sp.gameObject.transform, scanSpeed, orb);

            SetAction(() => {
                smallRight.Focus(t);
                smallLeft.Focus(t);
            }, p5time, () => {
                Transition();
                bigLeft.MoveTo(originLeft, moveTime);
                bigRight.MoveTo(originRight, moveTime);
                DelayThen(PhaseOneStart);
            });
        });
    }
}
