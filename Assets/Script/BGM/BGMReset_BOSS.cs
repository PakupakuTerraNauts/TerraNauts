using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMReset_BOSS : MonoBehaviour
{
    public AudioSource stageAudio;
    public AudioSource bossAudio;

    void Start(){
        stageAudio.volume = Volume.nowBGMVolume;
        bossAudio.Pause();
    }

    public void StageBGM_Stop(){
        stageAudio.Pause();
    }

    public void BossFightBGM_Start(){
        bossAudio.volume = Volume.nowBGMVolume;
        bossAudio.UnPause();
    }

    public void BossFightBGM_Stop(){
        bossAudio.Stop();
        stageAudio.volume = Volume.nowBGMVolume;
        stageAudio.UnPause();
    }
}
