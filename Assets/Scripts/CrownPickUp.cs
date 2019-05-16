using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrownPickUp : MonoBehaviour
{
    public string targetScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //FadeOut
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}
