using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColourPickup : MonoBehaviour
{
    public Vector3 colourRGB;
    public AudioSource au;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(colourRGB.x, colourRGB.y, colourRGB.z);
        au = GameObject.Find("AudioSource2").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        bool foundColour = false;
        if (other.tag == "Player")
        {
            SimplePlayer player = other.GetComponent<SimplePlayer>();

            player.changeAttackColour(colourRGB);
            au.Play();
            Destroy(gameObject);
        }

    }

}
