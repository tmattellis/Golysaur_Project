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

    public AudioClip flySound;
    public AudioClip sideSound;
    public AudioClip upSound;
    public AudioClip powerSound;
    public AudioClip whatSound;
    public AudioClip groundSound;

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

    public void PlaySoundFly()
    {
        audioSource.PlayOneShot(flySound);
    }
    public void PlaySoundSideEnemy()
    {
        audioSource.PlayOneShot(sideSound);
    }
    public void PlaySoundUpEnemy()
    {
        audioSource.PlayOneShot(upSound);
    }
    public void PlaySoundPowerup()
    {
        audioSource.PlayOneShot(powerSound);
    }
    public void PlaySoundWhat()
    {
        audioSource.PlayOneShot(whatSound);
    }
    public void PlaySoundGround()
    {
        audioSource.PlayOneShot(groundSound);
    }



}
