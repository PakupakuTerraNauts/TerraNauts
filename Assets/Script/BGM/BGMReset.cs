using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMReset : MonoBehaviour
{
    AudioSource _audio = null;

    void Start()
    {
        _audio = this.gameObject.GetComponent<AudioSource>();
        _audio.volume = Volume.nowBGMVolume;  // script\Menu\Volume.cs\nowVolume
        Debug.Log("audio:"+ _audio.volume+ "now:"+ Volume.nowBGMVolume);
    }
}
