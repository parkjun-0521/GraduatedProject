using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{
    // �Ҹ����� AudioMixer ������
    // Slider UI ���� ���� 
    public AudioMixer masterMixer;
    public Slider audioSlider;

    public void Start()
    {
        // �ʱ��� �Ҹ� ������ ���� 
        audioSlider.value = -20f;
    }
    public void AudioControl()
    {
        float sound = audioSlider.value;

        // �����̴� �� ���� Ȱ���Ͽ� ����ͼ��� ���� ���� 
        // �����̴��� ���� �����ϵ��� �����ϵ� 
        // �����̴��� ���� -40�� ��쿡�� ���带 ���� ���� ���� �ͼ��� ���� -80���� ���� 
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
