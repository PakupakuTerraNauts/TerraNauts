using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(AudioSource))]
public class Volume:MonoBehaviour
{
    public static float nowBGMVolume = 0.5f;
    public static float nowSEVolume = 1.0f;
    private AudioSource audioSource;
    private AudioSource audioSource_BOSS;
    private Slider BGMaudioSlider;
    private Slider SEaudioSlider;
    private GameObject BGM;


    private void Start()
    {
        BGM = GameObject.Find("BGM");
        audioSource = BGM.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        BGMaudioSlider = GameObject.Find("BGMVolume").GetComponent<Slider>();
        SEaudioSlider = GameObject.Find("SEVolume").GetComponent<Slider>();

        audioSource.volume = nowBGMVolume;
        BGMaudioSlider.value = nowBGMVolume;
        SEaudioSlider.value = nowSEVolume;


        try{    // �{�X�V�[���Ń{�X�퓬�pBGM���擾����
            audioSource_BOSS = BGM.transform.GetChild(1).gameObject.GetComponent<AudioSource>();
            audioSource_BOSS.volume = nowBGMVolume;
        }
        catch(Exception){
            return;
        }
    }

    // Menu�V�[����BGM�X���C�_�[���ω������Ƃ��ɃX���C�_�[����Ă΂��
    public void ChangeVolume()
    {
        audioSource.volume = BGMaudioSlider.value;
        if(audioSource_BOSS != null){
            audioSource_BOSS.volume = BGMaudioSlider.value;
        }
        nowBGMVolume = BGMaudioSlider.value;
        nowSEVolume = SEaudioSlider.value;
        Debug.Log("nowVolume : " + nowBGMVolume);
    }

}
