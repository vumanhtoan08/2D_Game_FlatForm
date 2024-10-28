using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio instance;
    public AudioSource GBSource;
    public AudioSource sfxSource;

    public AudioClip shootClip;
    public AudioClip reloadClip;
    public AudioClip killZbClip;
    public AudioClip completeClip;
    public AudioClip GBClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        PlayGBSource();
    }

    private void PlayGBSource()
    { 
        GBSource.clip = GBClip;
        GBSource.Play();
    }

    public void PlayShootClip()
    {
        sfxSource.PlayOneShot(shootClip);
    }
    public void PlayReloadClip()
    {
        sfxSource.PlayOneShot(reloadClip);
    }
    public void PlayZbDieClip()
    {
        sfxSource.PlayOneShot(killZbClip);
    }
    public void PlayCompeleteClip()
    {
        sfxSource.PlayOneShot(completeClip);
    }
}
