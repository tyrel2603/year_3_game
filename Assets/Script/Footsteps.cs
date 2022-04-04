using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip WalkSound;
    public AudioClip RotateSound;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }


    public void Walk()
    {
        if (!audio.isPlaying)
        {
            audio.clip = WalkSound;
            audio.Play();
        }
    }
    public void Rotate()
    {
        if (!audio.isPlaying)
        {
            audio.clip = RotateSound;
            audio.Play();
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
