using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMReset_BOSS : MonoBehaviour
{
    public AudioSource stageAudio;
    public AudioSource bossAudio;

    void Start(){
        stageAudio.volume = Volume.nowBGMVolume;
        // .Play()だと処理落ちするので.UnPause()で鳴らす→PlayOnAwakeにチェックしておく
        bossAudio.Pause();
    }

///<summary>
/// ボス部屋に入ったとき ステージBGMを止める
///</summary>
    public void StageBGM_Stop(){
        stageAudio.Pause();
    }

///<summary>
/// ボスBGMを鳴らす UnPause()を使用
///</summary>
    public void BossFightBGM_Start(){
        bossAudio.volume = Volume.nowBGMVolume;
        bossAudio.UnPause();
    }

///<summary>
/// ボスBGMを止め ステージBGMに戻す
///</summary>
    public void BossFightBGM_Stop(){
        bossAudio.Stop();
        stageAudio.volume = Volume.nowBGMVolume;
        stageAudio.UnPause();
    }
}
