using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedPickUp : MonoBehaviour
{
    public float power;
    public float duration;
    private AudioSource au;

    // Start is called before the first frame update
    void Start()
    {
        au = GameObject.Find("AudioSource2").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            TextRef.textBuilder.CreateText(transform.position, "+SPEED", Color.green, Color.black);
            SimplePlayer player = other.GetComponent<SimplePlayer>();

            player.speedPowerUp(duration, power);
            au.Play();
            Destroy(gameObject);
        }

    }
}
