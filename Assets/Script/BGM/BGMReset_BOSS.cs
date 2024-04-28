using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMReset_BOSS : MonoBehaviour
{
    public AudioSource stageAudio;
    public AudioSource bossAudio;

    void Start(){
        stageAudio.volume = Volume.nowVolume;
    }

    public void BossFightBGM_Start(){
        stageAudio.Pause();
        
        bossAudio.volume = Volume.nowVolume;
        bossAudio.Play();
    }

    public void BossFightBGM_Stop(){
        bossAudio.Stop();
        stageAudio.UnPause();
    }
}
