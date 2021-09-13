using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource source;
    public AudioClip song;
    public AudioClip gameOver;
    public AudioClip eatSFX;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playMainSong()
    {
        source.clip = song;
        if (!source.isPlaying)
        {
            source.Play();
        }
    }
    public void playEatSFX()
    {
        source.PlayOneShot(eatSFX);
    }
    public void playGameOverSong()
    {
        source.Stop();
        source.PlayOneShot(gameOver);
    }
}
