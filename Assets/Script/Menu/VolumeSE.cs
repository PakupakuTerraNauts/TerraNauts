using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSE : MonoBehaviour
{
    private Slider SEaudioSlider;

    void Start()
    {
        SEaudioSlider = GetComponent<Slider>();
        SEaudioSlider.value = GameManager.instance.nowVolumeSE;
    }

    public void ChangeVolumeSE(){
        GameManager.instance.ChangeVolumeSE(SEaudioSlider.value);
    }
}
