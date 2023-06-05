using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //在AudioManager裡面預先擺放的聲音Clips
    public Sound[] BGMSounds, sfxSounds;

    //在海RAKEY裡面負責出聲的物件
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
    private void Start()//一開始就撥放的BGM跟火車汽笛
    {
        PlayBGM("BGM");
        PlaySFX("StartTrainHorn");
    }
    public void Button_In()
    {
        PlaySFX("Button_In");
    }
    public void Bottun_Press()
    {
        PlaySFX("Button_Press");
    }
    public void PlayBGM(string name)//播放BGM的方法.要有想撥放BGM的字串,但因為現在只有一個BGM的Clips所以我只取名BGM而不是XX場景BGM
    {
        Sound s = Array.Find(BGMSounds, x=> x.name ==name);//在存放BGM的矩陣中找到要撥放的BGM名稱

        if(s == null)
        {
            Debug.Log("sound not found");//如果找不到就抱錯
        }
        else
        {
            BGMSource.clip = s.clip;//將海拉key中的Source替換成想撥放的BGM
            BGMSource.Play();//播放
        }
    }
    public void PlaySFX(string name)//播放音效的方法.要有想撥放音效的字串,如:Stun Rush ....
    {
        Sound s = Array.Find(sfxSounds, x=> x.name ==name);//在存放音效的矩陣中找到要撥放的音效的名稱

        if(s == null)
        {
            Debug.Log("sound not found");//如果找不到就抱錯
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);//播放
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
