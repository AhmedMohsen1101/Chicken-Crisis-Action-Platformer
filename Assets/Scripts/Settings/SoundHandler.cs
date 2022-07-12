using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SoundHandler : MonoBehaviour
{
    public AudioMixer defaultAudioMixer;
    private Slider soundSlider;

    private void OnEnable()
    {
        if(soundSlider==null)
            soundSlider = GetComponent<Slider>();

        soundSlider.onValueChanged.AddListener(OnSoundChange);
    }
    private void OnDisable()
    {
        soundSlider.onValueChanged.RemoveListener(OnSoundChange);
    }
    private void OnSoundChange(float value)
    {
        //change Sound 
        defaultAudioMixer.SetFloat("BackgroundMusic", value);
    }
}
