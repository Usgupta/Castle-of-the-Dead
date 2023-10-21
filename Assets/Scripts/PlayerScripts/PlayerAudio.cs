using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public Animator monkAnimator;
    public AudioSource monkAudio;
    public AudioClip monkJumpAudio;
    public AudioClip monkPunchAudio;
    public AudioClip monkKickAudio;

    public AudioClip monkDieAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayJumpSound()
    {
        monkAudio.PlayOneShot(monkJumpAudio);
    }

    public void PlayWalkSound()
    {
        monkAudio.PlayOneShot(monkAudio.clip);
    }

    public void PlayPunchSound()
    {
        monkAudio.PlayOneShot(monkPunchAudio);
    }

    public void PlayKickSound()
    {
        monkAudio.PlayOneShot(monkKickAudio);
    }

    public void PlayDieSound()
    {
        monkAudio.PlayOneShot(monkDieAudio);
    }
}
