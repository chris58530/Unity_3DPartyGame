using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    public Slider _BGMSlider, _SFXSlider;
    public void BGMVolume()
    {
        FindObjectOfType<AudioManager>().BGMVolume(_BGMSlider.value);
    }
    public void SFXVolume()
    {
        FindObjectOfType<AudioManager>().SFXVolume(_SFXSlider.value);
    }
    public void PlayClick()
    {
        FindObjectOfType<AudioManager>().PlayUI("Press2");

    }
    public void PlayPoint()
    {
        FindObjectOfType<AudioManager>().PlayUI("Press1");

    }

}
