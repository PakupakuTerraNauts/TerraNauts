using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int nowStage = 1;

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

    public void ChangeVolumeSE(float vol){
        _audio.volume = vol;
        nowVolumeSE = vol;
    }
}
