using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(AudioSource))]
public class Volume:MonoBehaviour
{
    public static float nowVolume = 0.5f;
    private AudioSource audioSource;
    private AudioSource audioSource_BOSS;
    private Slider audioSlider;
    private GameObject BGM;


    private void Start()
    {
        BGM = GameObject.Find("BGM");
        audioSource = BGM.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        audioSlider = GameObject.Find("Volume").GetComponent<Slider>();

        audioSource.volume = nowVolume;
        audioSlider.value = nowVolume;

        try{
            audioSource_BOSS = BGM.transform.GetChild(1).gameObject.GetComponent<AudioSource>();
            audioSource_BOSS.volume = nowVolume;
        }
        catch(Exception){
            return;
        }
    }

    public void ChangeVolume()
    {
        audioSource.volume = audioSlider.value;
        if(audioSource_BOSS != null){
            audioSource_BOSS.volume = audioSlider.value;
        }
        nowVolume = audioSlider.value;
        Debug.Log("nowVolume : " + nowVolume);
    }

}
