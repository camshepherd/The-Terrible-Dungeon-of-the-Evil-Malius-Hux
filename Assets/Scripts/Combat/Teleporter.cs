using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public float teleportDelay;
    public float xmin, xmax, ymin, ymax;

    public float timer;
    public bool teleporting;
    public float timeToTeleport;

    public StaticShooter shooter;

    private AudioSource au;
    public AudioClip teleportSound;

    public Vector3 colourRateChange;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        shooter = gameObject.GetComponent<StaticShooter>();
        au = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (shooter.working)
        {
            if (!teleporting && timer >= teleportDelay)
            {
                teleporting = true;
                gameObject.GetComponent<StaticShooter>().enabled = false;
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                colourRateChange = gameObject.GetComponent<Coloured>().colourRGB / timeToTeleport;
                au.clip = teleportSound;
                au.Play();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shooter.working)
        {
            timer += Time.deltaTime;

            if (teleporting && timer >= timeToTeleport)
            {
                Vector3 randPos = Camera.current.ViewportToWorldPoint(new Vector3(Random.Range(xmin, xmax), Random.Range(ymin, ymax), 0));
                randPos.z = 0;
                transform.position = randPos;
                gameObject.GetComponent<StaticShooter>().enabled = true;
                timer = 0;
                teleporting = false;
            }
        }
    }
}
