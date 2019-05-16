using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayedAudio : MonoBehaviour
{
    public float delay;
    AudioSource au;
    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
        StartCoroutine(playTheAudio());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator playTheAudio()
    {
        yield return new WaitForSeconds(delay);
        au.Play();
    }


}
