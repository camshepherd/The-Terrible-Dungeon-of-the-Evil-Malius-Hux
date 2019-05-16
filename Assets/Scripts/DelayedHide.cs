using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedHide : MonoBehaviour
{

    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        StartCoroutine(Wait(delay));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
        }
    }

    public IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
