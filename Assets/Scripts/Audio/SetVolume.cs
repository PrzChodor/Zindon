using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public string mixerParameter;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(mixerParameter, Mathf.Log10(sliderValue) * 20);
    }
}
