using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Fusion;

public class AudioManager : NetworkBehaviour
{
    public static AudioManager Instance;
    public Sound[] BGMSounds, sfxSounds, UISounds;
    public AudioSource BGMSource, sfxSource, UISourse;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        PlayBGM("BGM");
    }
    public void PlayBGM(string name)
    {
        Sound s = Array.Find(BGMSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            BGMSource.clip = s.clip;
            BGMSource.Play();
        }
    }
    public void PlayUI(string name)
    {
        Sound s = Array.Find(UISounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            UISourse.PlayOneShot(s.clip);
        }
    }
    [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
    public void RPC_PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }


    public void Button_In()
    {
        PlayUI("Press1");
    }
    public void Bottun_Press()
    {
        PlayUI("Press2");
    }
    public void BGMVolume(float volume)
    {
        BGMSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
        UISourse.volume = volume;
    }

    // {
    //     base.OnDestroy();
    // }

}
