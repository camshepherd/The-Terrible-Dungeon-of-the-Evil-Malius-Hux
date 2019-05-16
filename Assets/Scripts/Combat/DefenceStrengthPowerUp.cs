using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceStrengthPowerUp : MonoBehaviour
{
    public float fractionalIncrease;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            TextRef.textBuilder.CreateText(transform.position, "+DEFENCE", Color.blue, Color.black);
            SimplePlayer player = other.GetComponent<SimplePlayer>();
            player.alterDefencePower(fractionalIncrease);
            Destroy(gameObject);
        }
    }

}
