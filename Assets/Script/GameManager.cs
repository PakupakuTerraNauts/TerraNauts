using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private AudioSource _audio = null;
    
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
}
