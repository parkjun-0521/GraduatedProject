using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{
    // 소리조절 AudioMixer 변수와
    // Slider UI 변수 선언 
    public AudioMixer masterMixer;
    public Slider audioSlider;

    public void Start()
    {
        // 초기의 소리 고정값 지정 
        audioSlider.value = -20f;
    }
    public void AudioControl()
    {
        float sound = audioSlider.value;

        // 슬라이더 바 값을 활용하여 사운드믹서의 값을 지정 
        // 슬라이더의 값과 동일하도록 지정하되 
        // 슬라이더의 값이 -40일 경우에는 사운드를 끄기 위해 사운드 믹서의 값을 -80으로 지정 
        if (sound == -40f)
            masterMixer.SetFloat("TestMusic", -80f);
        else
            masterMixer.SetFloat("TestMusic", sound);
    }

    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}
