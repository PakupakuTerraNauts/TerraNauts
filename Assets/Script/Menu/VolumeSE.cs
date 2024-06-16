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

/// <summary>
/// MenuシーンのSEスライダーが変化したときにスライダーから呼ばれる
/// </summary>
    public void ChangeVolumeSE(){
        GameManager.instance.ChangeVolumeSE(SEaudioSlider.value);   // SEはすべてGMのオーディオを使用しているので変更しに行く
    }
}
