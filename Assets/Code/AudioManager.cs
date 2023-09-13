using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] public Sound[] musicSounds, sfxSounds;
    [SerializeField] public AudioSource musicSource, sfxSource;
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Problem Music");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Problem sfx");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
}