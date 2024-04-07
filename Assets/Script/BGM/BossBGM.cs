using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBGM : MonoBehaviour
{
    private AudioSource _audio = null;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void BossFightBGMStart(){
        _audio.Play();
    }

    public void BossFightBGMStop(){
        _audio.Stop();
    }
}
