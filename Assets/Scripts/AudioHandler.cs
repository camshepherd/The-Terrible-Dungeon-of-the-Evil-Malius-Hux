using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler : MonoBehaviour
{

    public List<AudioClip> clips;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        switch (GameStores.GetDepth())
        {
            case 1:
                source.clip = clips[0];
                break;
            case 2:
                source.clip = clips[1];
                break;
            case 3:
                source.clip = clips[2];
                break;
            case 4:
                source.clip = clips[3];
                break;
            case 5:
                source.clip = clips[4];
                break;
            case 6:
                source.clip = clips[5];
                break;
            default:
                source.clip = clips[5];
                break;
        }
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
