using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMReset : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        AudioSource _audio = this.gameObject.GetComponent<AudioSource>();
        _audio.volume = Volume.nowVolume;  // script\Menu\Volume.cs\nowVolume
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
