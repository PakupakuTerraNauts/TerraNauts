using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMReset_BOSS : MonoBehaviour
{
    public AudioSource stageAudio;
    public AudioSource bossAudio;

    void Start(){
        stageAudio.volume = Volume.nowBGMVolume;
        // .Play()���Ə�����������̂�.UnPause()�Ŗ炷��PlayOnAwake�Ƀ`�F�b�N���Ă���
        bossAudio.Pause();
    }

///<summary>
/// �{�X�����ɓ������Ƃ� �X�e�[�WBGM���~�߂�
///</summary>
    public void StageBGM_Stop(){
        stageAudio.Pause();
    }

///<summary>
/// �{�XBGM��炷 UnPause()���g�p
///</summary>
    public void BossFightBGM_Start(){
        bossAudio.volume = Volume.nowBGMVolume;
        bossAudio.UnPause();
    }

///<summary>
/// �{�XBGM���~�� �X�e�[�WBGM�ɖ߂�
///</summary>
    public void BossFightBGM_Stop(){
        bossAudio.Stop();
        stageAudio.volume = Volume.nowBGMVolume;
        stageAudio.UnPause();
    }
}
