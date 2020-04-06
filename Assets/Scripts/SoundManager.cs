using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource audioSource;
    public AudioClip fireballSound;
    public AudioClip arrowSound;
    public AudioClip impactSound;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySoundFireball()
    {
        audioSource.PlayOneShot(fireballSound);
    }
    public void PlaySoundArrow()
    {
        audioSource.PlayOneShot(arrowSound);
    }
    public void PlaySoundImpact()
    {
        audioSource.PlayOneShot(impactSound);
    }



}
