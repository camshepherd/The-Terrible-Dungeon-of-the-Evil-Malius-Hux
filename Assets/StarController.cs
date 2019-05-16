using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        int completes = GameStores.getGameComplete();
        //int completes = 1;
        if(completes>0)
        {
            sr.enabled = true;
        } else
        {
            sr.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.forward, Time.deltaTime * speed);
    }
}
