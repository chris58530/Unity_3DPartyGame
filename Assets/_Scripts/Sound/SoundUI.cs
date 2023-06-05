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
}
