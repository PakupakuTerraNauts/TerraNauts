using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public GameObject HPObject;

    void Update()
    {
        SetHPText();
        SetHpBar();
    }
    
    //HPテキストを表示
    public void SetHPText()
    {
        Text hptext = HPObject.GetComponent<Text>();
        int HP = StatusManager.HP;
        string maxHP = HP.ToString();
        string nowHP = StatusManager.nowHP.ToString();
        hptext.text = nowHP + "/" + maxHP;
    }

    //HPバーを表示
    public void SetHpBar()
    {
        GameObject _HPSlider = GameObject.Find("HPSlider");
        Slider HPSlider_S = _HPSlider.GetComponent<Slider>();
        int HP = StatusManager.HP;
        HPSlider_S.maxValue = HP;
        HPSlider_S.value = StatusManager.nowHP;
    }
}
