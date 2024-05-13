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

    public void StageBGM_Stop(){
        stageAudio.Pause();
    }

    public void BossFightBGM_Start(){
        bossAudio.volume = Volume.nowVolume;
        bossAudio.Play();
    }

    public void BossFightBGM_Stop(){
        bossAudio.Stop();
        stageAudio.volume = Volume.nowVolume;
        stageAudio.UnPause();
    }
}
