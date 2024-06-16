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
/// Menu�V�[����SE�X���C�_�[���ω������Ƃ��ɃX���C�_�[����Ă΂��
/// </summary>
    public void ChangeVolumeSE(){
        GameManager.instance.ChangeVolumeSE(SEaudioSlider.value);   // SE�͂��ׂ�GM�̃I�[�f�B�I���g�p���Ă���̂ŕύX���ɍs��
    }
}
