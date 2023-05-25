using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    public Slider _BGMSlider, _SFXSlider;
    public void BGMVolume()
    {
        AudioManager.Instance.BGMVolume(_BGMSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_SFXSlider.value);
    }
}
