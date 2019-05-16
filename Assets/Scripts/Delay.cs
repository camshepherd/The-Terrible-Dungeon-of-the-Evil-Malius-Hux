using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject thing in gameObjects)
        {
            thing.SetActive(false);
        }
        StartCoroutine(wait(delay));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (GameObject thing in gameObjects)
            {
                thing.SetActive(true);
            }
        }
    }

    IEnumerator wait(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach(GameObject thing in gameObjects)
        {
            thing.SetActive(true);
        }
    }
}
