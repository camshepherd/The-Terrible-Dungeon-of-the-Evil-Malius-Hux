using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthRestorePickUp : MonoBehaviour
{
    public float healthRestore;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SimplePlayer player = other.GetComponent<SimplePlayer>();
            player.healDamage(healthRestore);
            au.Play();
            Destroy(gameObject);
        }
        
    }
}
