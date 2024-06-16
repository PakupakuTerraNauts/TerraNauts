using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int nowStage = 1;  // セーブ機能に使用

    public float ninzinEXP = 80f;

    private AudioSource _audio = null;
    [HideInInspector] public float nowVolumeSE = 0.5f;
    
    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
    }

    void Start(){
        _audio = GetComponent<AudioSource>();
        _audio.volume = nowVolumeSE;
    }

///<summary>
/// SEを鳴らす
///</summary>
    public void PlaySE(AudioClip clip){

        if(_audio != null){
            _audio.PlayOneShot(clip);
        }
        else{
            Debug.Log("GM にAudioSourceがアタッチされていない");
        }
    }

/// <summary>
/// SE音量を変更したとき VolumeSE から呼ぶ
/// </summary>
/// <param name="vol">音量</param>
    public void ChangeVolumeSE(float vol){
        _audio.volume = vol;
        nowVolumeSE = vol;
    }
}
