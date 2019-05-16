using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColourChange : MonoBehaviour
{
    Camera camera;
    public float transitionTime;
    public Color target;

    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > transitionTime)
        {
            timer = 0;
            target = new Color(Random.Range(0.2f, 1), Random.Range(0.2f, 1), Random.Range(0.2f, 1),1);
        }
        camera.backgroundColor += ((target - camera.backgroundColor) / transitionTime) * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
    }
}
