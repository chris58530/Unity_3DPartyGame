using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] BGMSounds, sfxSounds;
    public AudioSource BGMSource, sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayBGM("BGM");
        PlaySFX("StartTrainHorn");
    }
    public void PlayBGM(string name)
    {
        Sound s = Array.Find(BGMSounds, x=> x.name ==name);

        if(s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            BGMSource.clip = s.clip;
            BGMSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x=> x.name ==name);

        if(s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void BGMVolume(float volume)
    {
        BGMSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

}
