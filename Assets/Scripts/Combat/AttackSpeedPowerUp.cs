using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedPowerUp : MonoBehaviour
{
    public float fractionalIncrease;

    public AudioSource au;

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
            SimplePlayer player = other.GetComponent<SimplePlayer>();

            TextRef.textBuilder.CreateText(transform.position, "+ATTACK SPEED", Color.red, Color.black);

            //player.attackSpeedPowerUp(duration, power);

            //Is now permanent power up
            player.PermAttackSpeedPowerUp(fractionalIncrease);
            Destroy(gameObject);
            au.Play();
        }

    }
}
