using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public Canvas canvas;

    public int E_maxHP;
    public int E_nowHP;

    public GameObject _HPSlider;



    void Update()
    {
        //EnemyCanvasをMain Cameraに向かせる
        canvas.transform.rotation = Camera.main.transform.rotation;
        SetHpBar();
    }

    public void SetHpBar()
    {
        Slider HPSlider_S = _HPSlider.GetComponent<Slider>();
        HPSlider_S.maxValue = E_maxHP;
        HPSlider_S.value = E_nowHP;
    }
}
