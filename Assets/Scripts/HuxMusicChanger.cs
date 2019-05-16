using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuxMusicChanger : MonoBehaviour
{

    public float introSongPeriod;

    public AudioClip[] clips;
    
    public float timer;

    private int clipRef = 0;
    AudioSource au;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        au = GetComponent<AudioSource>();
        au.clip = clips[0];
        au.Play();
        clipRef = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            timer = introSongPeriod + 1;
        }
        if(timer > introSongPeriod && clipRef == 0)
        {
            au.Stop();
            clipRef = (clipRef + 1) % clips.Length;
            au.clip = clips[clipRef];
            au.loop = true;
            au.Play();
        }
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
    }
}
